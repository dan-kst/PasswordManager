namespace PasswordManager.Models.Validation
{
    public static class AdminValidation
    {
        /* Admin Constraint Constants*/
        public const int MIN_NAME_LENGTH = 8;
        public const int MAX_NAME_LENGTH = 20;
        public const string INVALID_NAME_LENGTH = "Invalid Name length.";

        public const int MIN_PASSWORD_LENGTH = 8;
        public const int MAX_PASSWORD_LENGTH = 20;
        public const string INVALID_PASSWORD_LENGTH = "Invalid Password length.";

        public const string INVALID_EMAIL_ADDRESS = "Invalid Email Address.";
    }
}
