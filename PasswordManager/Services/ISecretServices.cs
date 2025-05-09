using PasswordManager.Models;

namespace PasswordManager.Services
{
    public interface ISecretServices
    {
        string GetSecret(string secretName);
        void SetSecret(string secretName, string secretValue);
        int CheckSecret(string secretName);
        void UpdateSecret(SecretBase secret, string secretValue);
    }
}
