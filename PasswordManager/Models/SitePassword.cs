using System.ComponentModel.DataAnnotations;

namespace PasswordManager.Models
{
    public class SitePassword : ISecretable
    {
        public int Id { get; set; }
        public int IClientId { get; set; }
        [Required]
        [Range(4, 100, ErrorMessage = "Invalid Name length.")]
        public string Name { get; set; }
        [Required]
        [Range(8, 20, ErrorMessage = "Invalid Password length.")]
        public string Value { get; set; }
        public EnumSecretQuality SecretQuality { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public string SiteUrl { get; set; }
        public IClient Client { get; set; }
        public SitePassword(IClient client, string value, string name, string url)
        {
            Client = client;
            IClientId = client.Id;
            Value = value;
            Name = name;
            SiteUrl = url;
            CreatedDate = DateTime.Now;
            LastUpdatedDate = DateTime.Now;
        }
    }
}
