using GREEDY.Models;
using GREEDY.Interfaces;
using System.Threading.Tasks;

namespace GREEDY.Controllers
{
    public class OCRController : iOCR
    {
        public Receipt UseOCR(string imageForOcrUrl)
        {
            Receipt receipt = new EmguOCR().UseOCR(imageForOcrUrl);
            return receipt;
        }

        public async Task<Receipt> UseOCRAsync(string imageForOcrUrl)
        {
            Receipt receipt =await Task.Run(()=>new EmguOCR().UseOCR(imageForOcrUrl));
            return receipt;
        }
    }
}
