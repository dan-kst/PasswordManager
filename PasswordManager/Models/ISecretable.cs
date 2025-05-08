using System.ComponentModel.DataAnnotations;

namespace PasswordManager.Models
{
    public interface ISecretable
    {
        public int Id { get; set; }
        public int IClientId { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        EnumSecretQuality SecretQuality { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public IClient Client { get; set; }
    }
}
