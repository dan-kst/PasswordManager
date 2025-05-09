using System.ComponentModel.DataAnnotations;

namespace PasswordManager.Models
{
    public class User : ClientBase
    {
        public ICollection<SecretBase>? Secrets { get; set; }
        public User() : base()
        {
            Secrets = null;
        }
    }
}
