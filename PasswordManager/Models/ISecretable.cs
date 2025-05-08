namespace PasswordManager.Models
{
    public interface ISecretable
    {
        int Id { get; set; }
        int ClientId { get; set; }
        string Name { get; set; }
        EnumSecretQuality SecretQuality { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdatedDate { get; set; }
    }
}
