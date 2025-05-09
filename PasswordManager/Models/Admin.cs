using System.ComponentModel.DataAnnotations;

namespace PasswordManager.Models
{
    public class Admin : ClientBase
    {
        public Admin() : base()
        {
            ClientType = EnumClientType.Admin;
        }
    }
}
