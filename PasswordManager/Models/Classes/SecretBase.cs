using System.ComponentModel.DataAnnotations;
using PasswordManager.Models.Enums;

namespace PasswordManager.Models.Classes
{
    public class SecretBase
    {
        public int Id { get; set; }
        public int IClientId { get; set; }
        [Display(Name = "Name")]
        public string? Name { get; set; }
        [Display(Name = "Value")]
        public string? Value { get; set; }
        [Display(Name = "Strength")]
        public EnumSecretQuality SecretQuality { get; set; }
        [Display(Name = "Type")]
        public EnumSecretType SecretType { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public ClientBase? Client { get; set; }
        public SecretBase()
        {
            SecretQuality = EnumSecretQuality.Weak;
            CreatedDate = DateTime.Now;
            LastUpdatedDate = DateTime.Now;
            SecretType = EnumSecretType.None;
        }
    }
}
