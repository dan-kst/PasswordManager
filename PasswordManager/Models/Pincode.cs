using System.ComponentModel.DataAnnotations;

namespace PasswordManager.Models
{
    public class Pincode : ISecretable
    {
        public int Id { get; set; }
        public int IClientId { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public EnumSecretQuality SecretQuality { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public IClient Client { get; set; }
        public Pincode(IClient client, string name, string value)
        {
            Client = client;
            IClientId = client.Id;
            Name = name;
            Value = value;
            CreatedDate = DateTime.Now;
            LastUpdatedDate = DateTime.Now;
        }
    }
}
