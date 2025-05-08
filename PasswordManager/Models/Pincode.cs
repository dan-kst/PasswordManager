namespace PasswordManager.Models
{
    public class Pincode : ISecretable
    {
        public int Value { get; set; }
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string Name { get; set; }
        public EnumSecretQuality SecretQuality { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public Pincode(int value)
        {
            Value = value;
            CreatedDate = DateTime.Now;
            LastUpdatedDate = DateTime.Now;
        }
    }
}
