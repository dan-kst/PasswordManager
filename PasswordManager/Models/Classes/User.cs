using System.ComponentModel.DataAnnotations;
using PasswordManager.Models.Enums;

namespace PasswordManager.Models.Classes
{
    public class User : ClientBase
    {
        public ICollection<SecretBase>? Secrets { get; set; }
        public User() : base()
        {
            ClientType = EnumClientType.Personal;
            Secrets = null;
        }
    }
}
