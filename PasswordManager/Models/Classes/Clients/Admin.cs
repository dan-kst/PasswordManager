using System.ComponentModel.DataAnnotations;
using PasswordManager.Models.Enums;
using PasswordManager.Models.Validation;

namespace PasswordManager.Models.Classes.Clients
{
    public class Admin : ClientBase
    {
        [Required]
        [StringLength(  AdminValidation.MAX_NAME_LENGTH, 
                        MinimumLength = AdminValidation.MIN_NAME_LENGTH, 
                        ErrorMessage = AdminValidation.INVALID_NAME_LENGTH)]
        public override string Name { get => base.Name; set => base.Name = value; }

        [Required]
        [EmailAddress(ErrorMessage = AdminValidation.INVALID_EMAIL_ADDRESS)]
        public override string Email { get => base.Email; set => base.Email = value; }

        [Required]
        [StringLength(  AdminValidation.MAX_PASSWORD_LENGTH, 
                        MinimumLength = AdminValidation.MIN_PASSWORD_LENGTH, 
                        ErrorMessage = AdminValidation.INVALID_PASSWORD_LENGTH)]
        public override string MasterPassword { get => base.MasterPassword; set => base.MasterPassword = value; }

        public int UsersDeleted { get; set; }
        public int UsersCreated { get; set; }
        public int UsersUpdated { get; set; }
        public Admin() : base()
        {
            ClientType = EnumClientType.Admin;
        }
    }
}
