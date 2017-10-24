using System.Drawing;

namespace GREEDY.Services
{
    public interface IImageFormating
    {
        Bitmap Blur(Bitmap bitmap, int width, int height);
    }
}
