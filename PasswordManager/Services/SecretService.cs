using PasswordManager.Contexts;
using PasswordManager.Models.Classes.Clients;
using System.Security.Claims;
using PasswordManager.Models.Classes.Secrets;
using Microsoft.EntityFrameworkCore;
using PasswordManager.Models.Enums;

namespace PasswordManager.Services
{
    public class SecretService
    {
        private readonly ClientContext _context;
        public SecretService(ClientContext context)
        {
            _context = context;
        }

        public async Task<List<SecretBase>?> GetFilteredSecretsAsync(string filterValue)
        {
            return await _context.Secrets
                .Where(c => c.Name != null && c.Name.StartsWith(filterValue))
                .ToListAsync();
        }

        public async Task<List<SecretBase>?> GetSecretsAsync(int clientId)
        {
            return await _context.Secrets
                .Where(c => c.ClientId.Equals(clientId))
                .ToListAsync();
        }

        public async Task<SecretBase?> GetSecretAsync(int Id, int clientId)
        {
            return await _context.Secrets
                .Where(c =>
                    c.Id.Equals(Id) &&
                    c.ClientId.Equals(clientId)
                    )
                .FirstOrDefaultAsync();
        }

        public async Task<bool> AddSecret(SecretBase secret)
        {
            switch(secret.SecretType)
            {
                case EnumSecretType.Pincode:
                    await AddSecret(secret as Pincode);
                    return true;
                case EnumSecretType.SitePassword:
                    await AddSecret(secret as SitePassword);
                    return true;
                default:
                    return false;
            }
        }

        public async Task<bool> AddSecret(SitePassword? sitePassword)
        {
            if (sitePassword != null)
            {
                _context.Secrets.Add(sitePassword);
                await _context.SaveChangesAsync();
                return true;
            }
            else
                return false;
        }
        public async Task AddSecret(Pincode? pincode)
        {
            if(pincode != null)
            {
                _context.Secrets.Add(pincode);
                
                await _context.SaveChangesAsync();
            }
        }


        public async Task<bool> DeleteSecret(int Id, int clientId)
        {
            var secret = await GetSecretAsync(Id, clientId);
            if(secret == null)
            {
                return false;
            }
            else
            {
                _context.Secrets.Remove(secret);
                await _context.SaveChangesAsync();
                return true;
            }
        }


        public async Task<bool> UpdateSecret(SitePassword sitePasssword)
        {
            var secret = await GetSecretAsync(sitePasssword.Id, sitePasssword.ClientId);
            if(secret == null || !secret.SecretType.Equals(sitePasssword.SecretType))
            {
                return false;
            }
            else
            {
                var result = secret as SitePassword;

                result.LastUpdatedDate = DateTime.Now;
                result.Value = sitePasssword.Value;
                result.Name = sitePasssword.Name;
                result.SecretQuality = sitePasssword.SecretQuality;
                result.SiteURL = sitePasssword.SiteURL;
                _context.SitePasswords.Update(result);
                await _context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<bool> UpdateSecret(Pincode pincode)
        {
            var secret = await GetSecretAsync(pincode.Id, pincode.ClientId);
            if (secret == null || !secret.SecretType.Equals(pincode.SecretType))
            {
                return false;
            }
            else
            {
                var result = secret as Pincode;

                result.LastUpdatedDate = DateTime.Now;
                result.Value = pincode.Value;
                result.Name = pincode.Name;

                _context.PinCodes.Update(result);
                await _context.SaveChangesAsync();
                return true;
            }
        }


        public async Task<bool> CheckSecretAsync(SecretBase secret)
        {
            var result = await _context.Secrets
                .Where(c =>
                    c.ClientId.Equals(secret.ClientId) &&
                    c.Name.Equals(secret.Name) &&
                    c.Value.Equals(secret.Value)
                    )
                .FirstOrDefaultAsync();

            return result != null;
        }

        public bool CompareSecret(SecretBase secret1, SecretBase secret2)
        {
            return  secret1.Name.Equals(secret2.Name) &&
                    secret1.Value.Equals(secret2.Value);
        }
    }
}
