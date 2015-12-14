/// <summary> 
/// <para> 版 权 : Copyright (c) 20010-2011 </para> 
/// <para> 项 目 : xxxxx/RD/xxxx </para> 
/// <para> 文件名称 : </para>
/// <para> 创 建 人 : lizhi </para>
/// <para> 创建日期 : 2010-11-22 </para>
/// <remarks> 备 注 : 
///     ddgooo@sina.com
/// </remarks>
/// </summary> 
///  
     public class Win32Message
    {
        /// <summary>
        /// 消息
        /// </summary>
        public const int WM_COPYDATA = 0x004A;

        /// <summary>
        /// WM_COPYDATA消息结构体
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct COPYDATASTRUCT
        {
            /// <summary>
            /// 用户定义数据
            /// </summary>
            public IntPtr   dwData;
            /// <summary>
            /// 数据大小
            /// </summary>
            public int      cbData;
            /// <summary>
            /// 指向数据的指针
            /// </summary>            
            public IntPtr lpData;
        }

        /// <summary>
        /// 注册消息用
        /// 在win7中，如果以管理员方式运行，需要加入该语句，注册WM_COPYDATA消息，避免过滤掉该消息。
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        [DllImport("user32")]
        public static extern bool ChangeWindowMessageFilter(int msg, int flags);
        

        /// <summary>
        /// 发送WM_COPYDATA消息
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="msg"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        [DllImport("User32.dll", EntryPoint = "SendMessage")]
        public static extern int SendMessage(int hWnd, int msg, IntPtr wParam, IntPtr lParam);
        /// <summary>
        /// 发送WM_COPYDATA消息
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="msg"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        [DllImport("user32")]
        public static extern bool PostMessage(int hWnd, int msg, IntPtr wParam, IntPtr lParam); 
                        
        /// <summary>
        /// 查找句柄
        /// </summary>
        /// <param name="lpClassName"></param>
        /// <param name="lpWindowName"></param>
        /// <returns></returns>
        [DllImport("User32.dll", EntryPoint = "FindWindow")]
        public static extern IntPtr FindWindow(string lpClassName,string lpWindowName);

        /// <summary>
        /// 查找句柄
        /// </summary>
        /// <param name="hwndParent"></param>
        /// <param name="hwndChildAfter"></param>
        /// <param name="lpszClass"></param>
        /// <param name="lpszWindow"></param>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "FindWindowEx")]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent,IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

    }