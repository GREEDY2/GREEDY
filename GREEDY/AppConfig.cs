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
        int MinUsernameLength { get; }
        int MinPasswordLength { get; }
        int MaxAnyInputLength { get; }
        string AuthenticationSecret { get; }
        string GoogleMapsGeocodingAPIKey { get; }
        string LogPath { get; }
        int ShowItemsInGraphs { get; }
    }

    public class AppConfig : IAppConfig
    {
        public string OcrLanguage => ConfigurationManager.AppSettings["OcrLanguage"];
        public string TesseractDataPath => ConfigurationManager.AppSettings["TessDataPath"];
        public string CategoriesDataPath => ConfigurationManager.AppSettings["CategoriesDataPath"];
        public string ServerAdressAndPort => ConfigurationManager.AppSettings["ServerAdressAndPort"];
        public string EncryptionKey => ConfigurationManager.AppSettings["EncryptionKey"];
        public int MinUsernameLength => int.Parse(ConfigurationManager.AppSettings["MinUsernameLength"]);
        public int MinPasswordLength => int.Parse(ConfigurationManager.AppSettings["MinPasswordLength"]);
        public int MaxAnyInputLength => int.Parse(ConfigurationManager.AppSettings["MaxAnyInputLength"]);
        public string AuthenticationSecret => ConfigurationManager.AppSettings["AuthenticationSecret"];
        public string GoogleMapsGeocodingAPIKey => ConfigurationManager.AppSettings["GoogleMapsGeocodingAPIKey"];
        public string LogPath => ConfigurationManager.AppSettings["LogPath"];
        public int ShowItemsInGraphs => int.Parse(ConfigurationManager.AppSettings["ShowItemsInGraphs"]);
    }
}