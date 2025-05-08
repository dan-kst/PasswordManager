namespace PasswordManager.Models
{
    public class SitePassword : ISecretable
    {
        public int Id { get; set; }
        public int IClientId { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public EnumSecretQuality SecretQuality { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public string SiteUrl { get; set; }
        public IClient Client { get; set; }
        public SitePassword(IClient client, string value, string name, string url)
        {
            Client = client;
            IClientId = client.Id;
            Value = value;
            Name = name;
            SiteUrl = url;
            CreatedDate = DateTime.Now;
            LastUpdatedDate = DateTime.Now;
        }
    }
}
