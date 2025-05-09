using System.ComponentModel.DataAnnotations;

namespace PasswordManager.Models
{
    public class Pincode : SecretBase
    {
        public Pincode() : base()
        {
            SecretType = EnumSecretType.Pincode;
        }
        public Pincode(ClientBase client, string name, string value)
        {
            Client = client;
            IClientId = client.Id;
            Name = name;
            Value = value;
            CreatedDate = DateTime.Now;
            LastUpdatedDate = DateTime.Now;
            SecretQuality = EnumSecretQuality.Strong;
        }
    }
}
