using System.ComponentModel.DataAnnotations;
using PasswordManager.Models.Classes.Secrets;
using PasswordManager.Models.Enums;
using PasswordManager.Models.Validation;

namespace PasswordManager.Models.Classes.Clients
{
    public class User : ClientBase
    {
        [Required]
        [StringLength(  UserValidation.MAX_NAME_LENGTH, 
                        MinimumLength = UserValidation.MIN_NAME_LENGTH, 
                        ErrorMessage = UserValidation.INVALID_NAME_LENGTH)]
        public override string Name { get => base.Name; set => base.Name = value; }
        
        [Required]
        [EmailAddress(ErrorMessage = UserValidation.INVALID_EMAIL_ADDRESS)]
        public override string Email { get => base.Email; set => base.Email = value; }
        
        [Required]
        [StringLength(  UserValidation.MAX_PASSWORD_LENGTH, 
                        MinimumLength = UserValidation.MIN_PASSWORD_LENGTH, 
                        ErrorMessage = UserValidation.INVALID_PASSWORD_LENGTH)]
        public override string MasterPassword { get => base.MasterPassword; set => base.MasterPassword = value; }
        
        public ICollection<SecretBase>? Secrets { get; set; }
       
        public int PasswordsDeleted { get; set; }
        public int PasswordsCreated { get; set; }
        public int PasswordsUpdated { get; set; }
        
        
        public User() : base()
        {
            ClientType = EnumClientType.Personal;
            Secrets = null;
        }
    }
}
