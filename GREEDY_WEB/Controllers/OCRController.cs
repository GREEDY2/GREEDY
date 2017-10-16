using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.AspNetCore.Http;
using System.Threading;

namespace GREEDY.Controllers
{
    public interface IFormFile
    {
        string ContentType { get; }
        string ContentDisposition { get; }
        IHeaderDictionary Headers { get; }
        long Length { get; }
        string Name { get; }
        string FileName { get; }
        Stream OpenReadStream();
        void CopyTo(Stream target);
        Task CopyToAsync(Stream target, CancellationToken cancellationToken);
    }

    [Route("api/[controller]")]
    public class OCRController : Controller
    {

        [HttpPost("[action]")]
        public async Task<IActionResult> OCR([FromBody]List<IFormFile> files)
        {
            long size = files.Sum(f => f.Length);

            // full path to file in temp location
            var filePath = Path.GetTempFileName();

            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        //await formFile.CopyToAsync(stream);
                    }
                }
            }

            // process uploaded files
            // Don't rely on or trust the FileName property without validation.

            return Ok(new { count = files.Count, size, filePath });
        }


        /*[HttpPost("[action]")]
        public string OCR([FromBody] byte[] image)
        {
            Console.WriteLine(image);
            Console.Beep();
            return "lol1";
        }*/

        /*[HttpPut("[action]")]
        public string OCR([FromBody] Stream image)
        {
            Console.WriteLine(image);
            Console.Beep();
            return "lol2";
        }*/
    }
}
