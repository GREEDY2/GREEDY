using GREEDY.Models;
using OpenCvSharp;

namespace GREEDY.Services
{
    public interface IReceiptService
    {
        Receipt ProcessReceiptImage(Mat image);
    }
}