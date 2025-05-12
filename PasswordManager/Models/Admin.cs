using System.ComponentModel.DataAnnotations;

namespace PasswordManager.Models
{
    public class Admin : IClient
    {
        private const int MIN_NAME_LENGTH = 4;
        private const int MAX_NAME_LENGTH = 100;

        private const int MIN_PASSWORD_LENGTH = 8;
        private const int MAX_PASSWORD_LENGTH = 20;

        public int Id { get; set; }
        [Required]
        [Range(MIN_NAME_LENGTH, MAX_NAME_LENGTH, ErrorMessage = "Invalid Name length.")]
        public string Name { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string Email { get; set; }
        [Required]
        [Range(MIN_PASSWORD_LENGTH, MAX_PASSWORD_LENGTH, ErrorMessage = "Invalid Password length.")]
        public string MasterPassword { get; set; }
        public Admin()
        {
            Id = 0;
            Name = string.Empty;
            Email = string.Empty;
            MasterPassword = string.Empty;
        }
    }
}
