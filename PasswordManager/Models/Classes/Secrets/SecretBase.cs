using System.ComponentModel.DataAnnotations;
using PasswordManager.Models.Classes.Clients;
using PasswordManager.Models.Enums;

namespace PasswordManager.Models.Classes.Secrets
{
    public class SecretBase
    {
        public int Id { get; set; }
        
        public int ClientId { get; set; }
        
        [Display(Name = "Name")]
        public virtual string? Name { get; set; }
        
        [Display(Name = "Value")]
        public virtual string? Value { get; set; }
        
        [Display(Name = "Type")]
        public virtual EnumSecretType SecretType { get; set; }
        
        [Display(Name = "Date of creation")]
        public DateTime CreatedDate { get; set; }
        
        [Display(Name = "Date of the last changing")]
        public DateTime LastUpdatedDate { get; set; }
        
        public ClientBase? Client { get; set; }
        
        
        public SecretBase()
        {
            CreatedDate = DateTime.Now;
            LastUpdatedDate = DateTime.Now;
        }
    }
}
