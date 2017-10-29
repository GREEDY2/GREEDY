using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GREEDY.DataManagers
{
    public interface IImageFormatting
    {
        Bitmap Format(Bitmap bitmap);
    }
}
