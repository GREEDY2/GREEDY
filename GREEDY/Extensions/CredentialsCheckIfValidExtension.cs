using System;
using System.Net.Mail;

namespace GREEDY.Extensions
{
    public static class CredentialsCheckIfValidExtension
    {
        private static readonly Lazy<int> MaxAnyInputLength = new Lazy<int>(
            () => Environments.AppConfig.MaxAnyInputLength);

        private static readonly Lazy<int> MinPasswordLength = new Lazy<int>(
            () => Environments.AppConfig.MinPasswordLength);

        private static readonly Lazy<int> MinUsernameLength = new Lazy<int>(
            () => Environments.AppConfig.MinUsernameLength);

        public static bool IsEmailValid(this string email)
        {
            if (email.Length > MaxAnyInputLength.Value) return false;
            try
            {
                var m = new MailAddress(email);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        public static bool IsUsernameValid(this string username)
        {
            return username.Length <= MaxAnyInputLength.Value && username.Length >= MinUsernameLength.Value;
        }

        public static bool IsPasswordValid(this string password)
        {
            return password.Length <= MaxAnyInputLength.Value && password.Length >= MinPasswordLength.Value;
        }
    }
}