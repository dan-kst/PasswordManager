using System.ComponentModel.DataAnnotations;

namespace PasswordManager.Models
{
    public class ClientBase
    {
        protected const int MIN_NAME_LENGTH = 4;
        protected const int MAX_NAME_LENGTH = 100;

        protected const int MIN_PASSWORD_LENGTH = 8;
        protected const int MAX_PASSWORD_LENGTH = 20;
        public int Id { get; set; }
        [Required]
        [StringLength(MAX_NAME_LENGTH, MinimumLength = MIN_NAME_LENGTH, ErrorMessage = "Invalid Name length.")]
        public string Name { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string Email { get; set; }
        [Required]
        [StringLength(MAX_PASSWORD_LENGTH, MinimumLength = MIN_PASSWORD_LENGTH,ErrorMessage = "Invalid Password length.")]
        public string MasterPassword { get; set; }
        public bool isNull { get; set; }
        public ClientBase()
        {
            Id = 0;
            Name = string.Empty;
            Email = string.Empty;
            MasterPassword = string.Empty;
            isNull = true;
        }
    }
}
