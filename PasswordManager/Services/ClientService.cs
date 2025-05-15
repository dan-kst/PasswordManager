using System.Net.Sockets;
using System.Security.Claims;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using PasswordManager.Contexts;
using PasswordManager.Models.Classes.Clients;
using PasswordManager.Models.Classes.Secrets;
using PasswordManager.Models.Enums;

namespace PasswordManager.Services
{
    public class ClientService
    {
        private readonly ClientContext _context;
        public ClientService(ClientContext context)
        {
            _context = context;
        }

        public async Task<ClientBase?> GetClientByIdAsync(int Id)
        {
            return await _context.Clients
                .Where(c => c.Id.Equals(Id))
                .FirstOrDefaultAsync();
        }

        public async Task<ClientBase?> GetClientByEmailAsync(string Email)
        {
            return await _context.Clients
                .Where(c => c.Email.Equals(Email))
                .FirstOrDefaultAsync();
        }

        public async Task<ClientBase?> GetClientAsync(ClientBase client)
        {
            return await _context.Clients
                .Where(c => c.Email.Equals(client.Email) &&
                            c.MasterPassword == client.MasterPassword)
                .FirstOrDefaultAsync();
        }


        public async Task<bool> AddClientAsync(ClientBase client)
        {
            switch (client.ClientType)
            {
                case EnumClientType.Personal:
                    return await AddClientAsync(client);
                case EnumClientType.Admin:
                    return await AddClientAsync(client);
                default:
                    return false;
            }
        }

        public async Task<bool> AddClientAsync(User? user)
        {
            if (user != null)
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                Console.WriteLine($"\n\n\n{user.Name}\n\n\n");
                return true;
            }
            else
                return false;
        }

        public async Task<bool> AddClientAsync(Admin? admin)
        {
            if (admin != null)
            {
                _context.Clients.Add(admin);
                await _context.SaveChangesAsync();
                return true;
            }
            else
                return false;
        }

        public async Task<bool> DeleteClientAsync(ClientBase client)
        {
            var secret = await GetClientByIdAsync(client.Id);
            if (secret == null)
            {
                return false;
            }
            else
            {
                _context.Clients.Remove(client);
                await _context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<bool> CheckClientAsync(ClientBase client)
        {
            var result = await _context.Clients
                .Where(c =>
                    c.Email.Equals(client.Email) &&
                    c.MasterPassword.Equals(client.MasterPassword)
                    )
                .FirstOrDefaultAsync();

            return result != null;
        }

        public async Task<bool> UpdateClientAsync(ClientBase client)
        {
            var result = await GetClientByIdAsync(client.Id);
            if (result == null)
            {
                return false;
            }
            else
            {
                result.LastUpdatedDate = DateTime.Now;
                _context.Clients.Update(result);
                await _context.SaveChangesAsync();
                return true;
            }
        }

        public bool CompareClientAsync(ClientBase client1, ClientBase client2)
        {
            return client1.Name.Equals(client2.Name) &&
                   client1.Email.Equals(client2.Email) &&
                   client1.MasterPassword.Equals(client2.MasterPassword);
        }

        public ClaimsPrincipal AuthenticateClient(ClientBase client)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, client.Name),
                new Claim(ClaimTypes.Email, client.Email),
                new Claim(ClaimTypes.Role, client.ClientType.ToString()),
                new Claim(ClaimTypes.NameIdentifier, client.Id.ToString())
            };
            var identity = new ClaimsIdentity(claims, "PasswordManagerAuth");
            return new ClaimsPrincipal(identity);

        }
    }
}