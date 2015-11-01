using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;

namespace Capture_LikeQQ
{
   public class Capture
    {
       [DllImport("user32.dll", SetLastError = true)]
       public static extern bool GetCursorPos(ref Point point);
       public static Bitmap GetImage(Point upperleft,Size size)
       {
           Bitmap bmp = new Bitmap(size.Width,size.Height);
           Graphics g = Graphics.FromImage(bmp);
           g.CopyFromScreen(upperleft.X, upperleft.Y, 0, 0, size);
           return bmp;
       }
    }
}
