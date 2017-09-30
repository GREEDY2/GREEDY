using System.Configuration;

namespace GREEDY
{
    public interface IAppConfig
    {
        string OcrLanguage { get; }
        string TesseractDataPath { get; }
        string FilterImageFiles { get; }
        string SaveFilePath { get; }
    }

    public class AppConfig : IAppConfig
    {
        public string OcrLanguage => ConfigurationManager.AppSettings["OcrLanguage"];
        public string TesseractDataPath => ConfigurationManager.AppSettings["TessDataPath"];
        public string FilterImageFiles => ConfigurationManager.AppSettings["FilterImageFiles"];
        public string SaveFilePath => ConfigurationManager.AppSettings["SaveFilePath"];
    }
}