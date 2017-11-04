using System.Configuration;

namespace GREEDY
{
    public interface IAppConfig
    {
        string OcrLanguage { get; }
        string TesseractDataPath { get; }
        string FilterImageFiles { get; }
        string SaveFilePath { get; }
        string CategoriesDataPath { get; }
        string ServerAdressAndPort { get; }
        string UsersDataPath { get; }
        string EncryptionKey { get; }
        string MinUsernameLength { get; }
        string MinPasswordLength { get; }
        string MaxAnyInputLength { get; }
    }

    public class AppConfig : IAppConfig
    {
        public string OcrLanguage => ConfigurationManager.AppSettings["OcrLanguage"];
        public string TesseractDataPath => ConfigurationManager.AppSettings["TessDataPath"];
        public string FilterImageFiles => ConfigurationManager.AppSettings["FilterImageFiles"];
        public string SaveFilePath => ConfigurationManager.AppSettings["SaveFilePath"];
        public string CategoriesDataPath => ConfigurationManager.AppSettings["CategoriesDataPath"];
        public string ServerAdressAndPort => ConfigurationManager.AppSettings["ServerAdressAndPort"];
        public string UsersDataPath => ConfigurationManager.AppSettings["UsersDataPath"];
        public string EncryptionKey => ConfigurationManager.AppSettings["EncryptionKey"];
        public string MinUsernameLength => ConfigurationManager.AppSettings["MinUsernameLength"];
        public string MinPasswordLength => ConfigurationManager.AppSettings["MinPasswordLength"];
        public string MaxAnyInputLength => ConfigurationManager.AppSettings["MaxAnyInputLength"];
    }
}