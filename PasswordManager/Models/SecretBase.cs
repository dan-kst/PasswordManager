using System.ComponentModel.DataAnnotations;

namespace PasswordManager.Models
{
    public class SecretBase
    {
        public int Id { get; set; }
        public int IClientId { get; set; }
        public string? Name { get; set; }
        public string? Value { get; set; }
        public EnumSecretQuality SecretQuality { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public ClientBase? Client { get; set; }
        public bool isNull { get; set; }
        public SecretBase()
        {
            SecretQuality = EnumSecretQuality.Weak;
            CreatedDate = DateTime.Now;
            LastUpdatedDate = DateTime.Now;
            isNull = true;
        }
    }
}
