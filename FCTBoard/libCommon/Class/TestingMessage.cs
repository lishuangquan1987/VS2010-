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
    /// <summary>
    /// 脚本测试类
    /// </summary> 
    public class TestingMessage
    {   
        /// <summary>
        /// 消息 开始
        /// </summary>
        public const int WM_USER_START = 0x04DD;
        /// <summary>
        /// 消息 继续
        /// </summary>
        public const int WM_USER_CONTINUE = 0x05F8;
        /// <summary>
        /// 消息 停止
        /// </summary>
        public const int WM_USER_ABORT = 0x06F9;

        /// <summary>
        /// 最后的消息数据
        /// </summary>
        public string testingScript = null;

        /// <summary>
        /// 初始化
        /// </summary>
        public bool tetsingEnable = false;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns></returns>
        public bool Initiate()
        {    
            Win32Message.ChangeWindowMessageFilter(Win32Message.WM_COPYDATA, 1);

            tetsingEnable = true;

            return true;
        }

        /// <summary>
        /// 解析消息
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public bool DoMessage(ref System.Windows.Forms.Message msg)
        {
            bool tested = false;

            switch (msg.Msg)
            {
                case Win32Message.WM_COPYDATA :
                    DoMsgCopyData(ref msg);
                    tested = true;
                    break;

                case WM_USER_START :
                    DoMsgStart(ref msg);
                    tested = true;
                    break;

                case WM_USER_CONTINUE :
                    DoMsgContinue(ref msg);
                    tested = true;
                    break;

                case WM_USER_ABORT :
                    DoMsgAbort(ref msg);
                    tested = true;
                    break;

                default :
                    break;
            }

            return tested;
        }
        
        /// <summary>
        /// 处理拷贝数据
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public bool DoMsgCopyData(ref System.Windows.Forms.Message msg)
        {
            bool tested = false;

            if (tetsingEnable)
            {
                try
                {
                    Win32Message.COPYDATASTRUCT mystr = new Win32Message.COPYDATASTRUCT();
                    Type mytype = mystr.GetType();
                    mystr = (Win32Message.COPYDATASTRUCT)msg.GetLParam(mytype);
                    Byte[] scriptbytes = new Byte[mystr.cbData];
                    Marshal.Copy(mystr.lpData, scriptbytes, 0, mystr.cbData);
                    System.Text.Encoding utf8 = System.Text.Encoding.UTF8;
                    testingScript = utf8.GetString(scriptbytes);

                    System.Windows.Forms.MessageBox.Show(testingScript);

                    tested = true;
                }
                catch
                {
                    tested = false;
                }
            }

            return tested;
        }


        /// <summary>
        /// 开始
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public bool DoMsgStart(ref System.Windows.Forms.Message msg)
        {
            bool tested = false;

            if (tetsingEnable)
            {
                try
                {
                    if (testingScript != null)
                    {
                        tested = XXXX.Parser.TestingParser.Instance.TestRunning(testingScript);

                        System.Windows.Forms.MessageBox.Show("SERVER DoMsgStart");
                    }
                }
                catch
                {
                    tested = false;
                }
            }

            return tested;
        }

        
        /// <summary>
        /// 继续
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public bool DoMsgContinue(ref System.Windows.Forms.Message msg)
        {
            bool tested = false;

            if (tetsingEnable)
            {
                try
                {
                    if (testingScript != null)
                    {
                        tested =  XXXX .Parser.TestingParser.Instance.TestContinue();

                        System.Windows.Forms.MessageBox.Show("SERVER DoMsgContinue");
                    }
                }
                catch
                {
                    tested = false;
                }
            }

            return tested;
        }


        /// <summary>
        /// 继续
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public bool DoMsgAbort(ref System.Windows.Forms.Message msg)
        {
            bool tested = false;

            if (tetsingEnable)
            {
                try
                {
                    if (testingScript != null)
                    {
                        tested =  XXXX .Parser.TestingParser.Instance.TestAbort();
                        testingScript = null;

                        System.Windows.Forms.MessageBox.Show("SERVER DoMsgAbort");
                    }
                }
                catch
                {
                    tested = false;
                }
            }

            return tested;
        }

    }