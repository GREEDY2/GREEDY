using System;
using System.Net.Mail;

namespace GREEDY.Extensions
{
    public static class CredentialsCheckIfValidExtension
    {
        static Lazy<int> MaxAnyInputLength = new Lazy<int>(
            () => int.Parse(Environments.AppConfig.MaxAnyInputLength));
        static Lazy<int> MinPasswordLength = new Lazy<int>(
            () => int.Parse(Environments.AppConfig.MinPasswordLength));
        static Lazy<int> MinUsernameLength = new Lazy<int>(
            () => int.Parse(Environments.AppConfig.MinUsernameLength));

        public static bool IsEmailValid(this string email)
        {
            if (email.Length > MaxAnyInputLength.Value)
            {
                return false;
            }
            try
            {
                MailAddress m = new MailAddress(email);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        public static bool IsUsernameValid(this string username)
        {
            if (username.Length > MaxAnyInputLength.Value
                || username.Length < MinUsernameLength.Value)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool IsPasswordValid(this string password)
        {
            if (password.Length > MaxAnyInputLength.Value 
                || password.Length < MinPasswordLength.Value)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
