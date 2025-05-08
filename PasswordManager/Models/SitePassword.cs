using System.ComponentModel.DataAnnotations;

namespace PasswordManager.Models
{
    public class SitePassword : ISecretable
    {
        private const int MIN_NAME_LENGTH = 4;
        private const int MAX_NAME_LENGTH = 100;

        private const int MIN_PASSWORD_LENGTH = 8;
        private const int MAX_PASSWORD_LENGTH = 20;
        public int Id { get; set; }
        public int IClientId { get; set; }
        [Required]
        [Range(MIN_NAME_LENGTH, MAX_NAME_LENGTH, ErrorMessage = "Invalid Name length.")]
        public string Name { get; set; }
        [Required]
        [Range(MIN_PASSWORD_LENGTH, MAX_PASSWORD_LENGTH, ErrorMessage = "Invalid Password length.")]
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
