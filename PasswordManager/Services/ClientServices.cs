using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using PasswordManager.Contexts;
using PasswordManager.Models.Classes.Clients;

namespace PasswordManager.Services
{
    public class ClientServices
    {
        private readonly ClientContext _context;
        public ClientServices(ClientContext context)
        {
            _context = context;
        }

        public async Task<bool> IsClientExistAsync(ClientBase client)
        {
            return await _context.Users
                .AnyAsync(c =>
                    c.Name.Equals(client.Name) &&
                    c.Email.Equals(client.Email) &&
                    c.MasterPassword.Equals(client.MasterPassword)
                    );
        }
        public async Task<ClientBase?> GetClientAsync(ClientBase client)
        {
            //Console.WriteLine($"\n\n\n\n{client.Name} {client.Email} {client.MasterPassword}\n\n\n\n");
            //await _context.Clients.ForEachAsync(c => Console.WriteLine($"\n\n{c.Name} {c.Email} {c.MasterPassword}\n\n"));
            return await _context.Clients
                .Where(c =>
                    c.Email.Equals(client.Email) &&
                    c.MasterPassword.Equals(client.MasterPassword)
                    )
                .FirstOrDefaultAsync();
        }

        public async Task<User?> GetUserAsync(ClientBase client)
        {
            return await _context.Users
                .Where(c =>
                    c.Email.Equals(client.Email) &&
                    c.MasterPassword.Equals(client.MasterPassword)
                    )
                .FirstOrDefaultAsync();
        }

        public async Task<Admin?> GetAdminAsync(ClientBase client)
        {
            return await _context.Admins
                .Where(c =>
                    c.Email.Equals(client.Email) &&
                    c.MasterPassword.Equals(client.MasterPassword)
                    )
                .FirstOrDefaultAsync();
        }

        public void SetClient(ClientBase client)
        {

        }

        public int CheckClient(ClientBase client)
        {
            return 0;
        }

        public void UpdateClient(ClientBase secret, string secretValue)
        {
        }

        public bool CompareClient(ClientBase client1, ClientBase client2)
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