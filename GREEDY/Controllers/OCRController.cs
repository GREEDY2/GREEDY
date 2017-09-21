using GREEDY.Models;
using GREEDY.Interfaces;

namespace GREEDY.Controllers
{
    public class OCRController : iOCR
    {
        public Receipt UseOCR(string imageForOcrUrl)
        {
            Receipt receipt = new EmguOCR().UseOCR(imageForOcrUrl);
            return receipt;
        }
    }
}
