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
    }

    public class AppConfig : IAppConfig
    {
        public string OcrLanguage => ConfigurationManager.AppSettings["OcrLanguage"];
        public string TesseractDataPath => ConfigurationManager.AppSettings["TessDataPath"];
        public string CategoriesDataPath => ConfigurationManager.AppSettings["CategoriesDataPath"];
        public string ServerAdressAndPort => ConfigurationManager.AppSettings["ServerAdressAndPort"];
        public string EncryptionKey => ConfigurationManager.AppSettings["EncryptionKey"];
    }
}