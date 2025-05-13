using System.ComponentModel.DataAnnotations;
using PasswordManager.Models.Enums;

namespace PasswordManager.Models.Classes.Clients
{
    public class ClientBase
    {
        public int Id { get; set; }
        [Display(Name = "Username")]
        public virtual string Name { get; set; }
        [Display(Name = "Email")]
        public virtual string Email { get; set; }
        [Display(Name = "Password")]
        public virtual string MasterPassword { get; set; }
        [Display(Name = "Type")]
        public virtual EnumClientType ClientType { get; set; }
        [Display(Name = "Date of creation")]
        public DateTime CreatedDate { get; set; }
        [Display(Name = "Date of the last changing")]
        public DateTime LastUpdatedDate { get; set; }
        public ClientBase()
        {
            Id = 0;
            Name = string.Empty;
            Email = string.Empty;
            MasterPassword = string.Empty;
            ClientType = EnumClientType.None;
            CreatedDate = DateTime.Now;
            LastUpdatedDate = DateTime.Now;
        }
    }
}
