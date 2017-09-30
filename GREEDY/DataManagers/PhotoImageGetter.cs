using System.Drawing;

namespace GREEDY.DataManagers
{
    class PhotoImageGetter : IImageGetter
    {
        private static IAppConfig AppConfig => new AppConfig();

        /// <summary>
        /// Gets image from camera using Emgu.CV
        /// </summary>
        /// <returns></returns>
        public Bitmap GetImage()
        {
            return null;
        }
    }
}
