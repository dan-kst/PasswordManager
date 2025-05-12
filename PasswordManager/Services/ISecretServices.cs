using PasswordManager.Models.Classes;

namespace PasswordManager.Services
{
    public interface ISecretServices
    {
        public string GetSecret(string secretName);
        public void SetSecret(string secretName, string secretValue);
        public int CheckSecret(string secretName);
        public void UpdateSecret(SecretBase secret, string secretValue);
    }
}
