using PasswordManager.Models;

namespace PasswordManager.Services
{
    public class UserServices : IClientServices
    {
        private readonly User _user;
        public UserServices(User user)
        {
            _user = user;
        }
        public string GetClient(string secretName)
        {
            return string.Empty;
        }
        public void SetClient(string secretName, string secretValue)
        {

        }
        public int CheckClient(string secretName)
        {
            return 0;
        }
        public void UpdateClient(ClientBase secret, string secretValue)
        {

        }
        public bool CompareClient(ClientBase client)
        {
            User _client = client as User;

            return _user.Name.Equals(_client.Name) &&
                   _user.Email.Equals(_client.Email) &&
                   _user.MasterPassword.Equals(_client.MasterPassword);
        }
    }
}