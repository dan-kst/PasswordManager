using System.ComponentModel.DataAnnotations;

namespace PasswordManager.Models
{
    public class Pincode : ISecretable
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public EnumSecretQuality SecretQuality { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public Pincode(string name, string value)
        {
            Name = name;
            Value = value;
            CreatedDate = DateTime.Now;
            LastUpdatedDate = DateTime.Now;
        }
    }
}
