using System.ComponentModel.DataAnnotations;

namespace PasswordManager.Models
{
    public class SitePassword : SecretBase
    {
        public string? SiteUrl { get; set; }
        public SitePassword() : base()
        {
            
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
            isNull = false;
        }
    }
}
