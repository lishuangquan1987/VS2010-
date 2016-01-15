using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace 图片特效切换
{
  public class DLLHandler
    {
        [DllImport(@"clayui_forcsharp.dll")]
        public static extern void CLAYUI_CSharp_Init(IntPtr handle);

        [DllImport(@"clayui_forcsharp.dll")]
         static extern void CLAYUI_CSharp_Release();

        [DllImport(@"clayui_forcsharp.dll")]
        public static extern void CLAYUI_OnAnimation(IntPtr handle, int vert, int flag, int anitype, int invert);

        [DllImport(@"clayui_forcsharp.dll")]
        public static extern void Redraw(IntPtr handle, int usetime);

        [DllImport(@"clayui_forcsharp.dll")]
        public static extern int IsPlay();

        [DllImport(@"clayui_forcsharp.dll")]
        public static extern void CLAYUI_InitDialog2(IntPtr handle, IntPtr handle1);

        [DllImport(@"clayui_forcsharp.dll")]
        public static extern void MakeWindowTpt(IntPtr handle, int factor);

        [DllImport(@"clayui_forcsharp.dll")]
        public static extern void WinRedraw(IntPtr handle, int redraw);

        [DllImport(@"clayui_forcsharp.dll")]
        public static extern void desktomemdc1(IntPtr handle);

       

    }
}
