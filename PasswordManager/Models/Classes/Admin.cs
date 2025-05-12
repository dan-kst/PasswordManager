using System.ComponentModel.DataAnnotations;
using PasswordManager.Models.Enums;

namespace PasswordManager.Models.Classes
{
    public class Admin : ClientBase
    {
        
        public Admin() : base()
        {
            ClientType = EnumClientType.Admin;
        }
    }
}
