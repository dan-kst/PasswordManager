using System.ComponentModel.DataAnnotations;
using PasswordManager.Models.Classes.Clients;
using PasswordManager.Models.Enums;

namespace PasswordManager.Models.Classes.Secrets
{
    public class SitePassword : SecretBase
    {
        public string? SiteUrl { get; set; }
        public SitePassword() : base()
        {
            SecretType = EnumSecretType.SitePassword;
        }
        public SitePassword(ClientBase client, string value, string name, string url)
        {
            Client = client;
            IClientId = client.Id;
            Value = value;
            Name = name;
            SiteUrl = url;
            CreatedDate = DateTime.Now;
            LastUpdatedDate = DateTime.Now;
            SecretType = EnumSecretType.SitePassword;
        }
    }
}
