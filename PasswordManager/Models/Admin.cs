namespace PasswordManager.Models
{
    public class Admin : IClient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
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
