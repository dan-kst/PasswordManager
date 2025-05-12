using PasswordManager.Models.Classes;

namespace PasswordManager.Services
{
    public interface IClientServices
    {
        public string GetClient(string secretName);
        public void SetClient(string secretName, string secretValue);
        public int CheckClient(string secretName);
        public void UpdateClient(ClientBase secret, string secretValue);
        public bool CompareClient(ClientBase client);
    }
}
