using System.Drawing;

namespace GREEDY.DataManagers
{
    class PhotoImageGetter : IImageGetter
    {
        private readonly IAppConfig _config;

        public PhotoImageGetter(IAppConfig config)
        {
            _config = config;
        }

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
