using System.Configuration;

namespace GREEDY
{
    public interface IAppConfig
    {
        string OcrLanguage { get; }
        string TesseractDataPath { get; }
        string CategoriesDataPath { get; }
        string ServerAdressAndPort { get; }
        string EncryptionKey { get; }
        string MinUsernameLength { get; }
        string MinPasswordLength { get; }
        string MaxAnyInputLength { get; }
        string AuthenticationSecret { get; }
    }

    public class AppConfig : IAppConfig
    {
        public string OcrLanguage => ConfigurationManager.AppSettings["OcrLanguage"];
        public string TesseractDataPath => ConfigurationManager.AppSettings["TessDataPath"];
        public string CategoriesDataPath => ConfigurationManager.AppSettings["CategoriesDataPath"];
        public string ServerAdressAndPort => ConfigurationManager.AppSettings["ServerAdressAndPort"];
        public string EncryptionKey => ConfigurationManager.AppSettings["EncryptionKey"];
        public string MinUsernameLength => ConfigurationManager.AppSettings["MinUsernameLength"];
        public string MinPasswordLength => ConfigurationManager.AppSettings["MinPasswordLength"];
        public string MaxAnyInputLength => ConfigurationManager.AppSettings["MaxAnyInputLength"];
        public string AuthenticationSecret => ConfigurationManager.AppSettings["AuthenticationSecret"];
    }
}