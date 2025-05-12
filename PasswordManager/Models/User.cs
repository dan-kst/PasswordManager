namespace PasswordManager.Models
{
    public class User : IClient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string MasterPassword { get; set; }
        public List<ISecretable>? Secrets { get; set; }
        public User()
        {
            Id = 0;
            Name = string.Empty;
            Email = string.Empty;
            MasterPassword = string.Empty;
            Secrets = null;
        }
    }
}
