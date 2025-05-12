using System.ComponentModel.DataAnnotations;
using PasswordManager.Models.Classes.Clients;
using PasswordManager.Models.Enums;

namespace PasswordManager.Models.Classes.Secrets
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
