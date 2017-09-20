using GREEDY.Models;
using GREEDY.Interfaces;

namespace GREEDY.Controllers
{
    public class OCRController : iOCR
    {
        public Receipt UseOCR(string url)
        {
            Receipt receipt = new EmguOCR().UseOCR(url);
            return receipt;
        }
    }
}
