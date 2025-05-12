using System.ComponentModel.DataAnnotations;
using PasswordManager.Models.Enums;

namespace PasswordManager.Models.Classes.Clients
{
    public class ClientBase
    {
        public int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Email { get; set; }
        public virtual string MasterPassword { get; set; }
        public EnumClientType ClientType { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public ClientBase()
        {
            Id = 0;
            Name = string.Empty;
            Email = string.Empty;
            MasterPassword = string.Empty;
            ClientType = EnumClientType.None;
        }
    }
}
