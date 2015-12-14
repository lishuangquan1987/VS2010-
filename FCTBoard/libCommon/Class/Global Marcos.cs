using System;
namespace TestStudio.Automation.TestManager.libCommon.Class
{
    public enum USER_AUTHORITY
    {
        Root=-2,    //super user
        Administrator=-1,
        Developer=0,
        Engineer,
        Operator,
    }

    public class User_Infor 
    {
        public string Name;
        public string Password;
        public USER_AUTHORITY Authority;
    }

    public static class tmMarcos
    {
        //Notification
        public const string kOnEngineStart = "kOnEngineStart";
        public const string kOnEngineStop = "kOnEngineStop";
        public const string kOnTestStart = "kOnTestStart";
        public const string kOnTestStop = "kOnTestStop";
        public const string kOnTestError = "kOnTestError";
        public const string kOnTestPause = "kOnTestPause";
        public const string kOnTestResume = "kOnTestResume";
        public const string kOnTestFinish = "kOnTestFinish";
        public const string kOnTestItemStart = "kOnTestItemStart";
        public const string kOnTestItemFinish = "kOnTestItemFinish";
        public const string kOnTestItemFinishEx = "kOnTestItemFinishEx";

        public const string kOnUiLoadFinish = "kOnUiLoadFinish";
        public const string kOnLoadProfile = "kOnLoadProfile";
        public const string kOnUiUnLoad = "kOnUiUnLoad";
        public const string kOnUUTEnableChange = "kOnUUTEnableChange";

        public const string kLoadLeftPulsarProfile = "kLoadLeftPulsarProfile"; //通知加载校准的profile
        public const string kLoadRightPulsarProfile = "kLoadRightPulsarProfile";

        public const string kDegbugMessage = "kDegbugMessage";
        public const string kTestFlowMessage = "kTestFlowMessage";

        public const string kOnChangeUser = "kOnChangeUser";
        public const string kOnShowBarCodeScan = "kOnShowBarCodeScan";


        public const string kOnStartTest = "kOnStartTest";
        public const string kOnStartDetect = "kOnStartDetect";
        public const string kOnGetSN     = "kOnTestGetSN";

        public const string kOnCtrlLamp = "kOnCtrlLamp";
        public const string kOnUpdateArMinMax = "kOnUpdateArMinMax";
             
        //Key


        //Configuration
        public const string kUUTEnable = "kUUTEnable";
        public const string kUUTEnable0 = "kUUTEnable0";
        public const string kUUTEnable1 = "kUUTEnable1";
        public const string kUUTEnable2 = "kUUTEnable2";
        public const string kUUTEnable3 = "kUUTEnable3";
        public const string kUUTEnable4 = "kUUTEnable4";
        public const string kUUTEnable5 = "kUUTEnable5";
        public const string kUUTEnable6 = "kUUTEnable6";
        public const string kUUTEnable7 = "kUUTEnable7";
        public const string kUUTEnable8 = "kUUTEnable8";
        public const string kUUTEnable9 = "kUUTEnable9";
        public const string kUUTEnable10 = "kUUTEnable10";
        public const string kUUTEnable11 = "kUUTEnable11";
        public const string kUUTEnabl12 = "kUUTEnable12";
        public const string kUUTEnabl13 = "kUUTEnable13";
        public const string kUUTEnabl14 = "kUUTEnable14";
        public const string kUUTEnabl15 = "kUUTEnable15";

        public const string kConfigLogDir = "LogDir";
        public const string kConfigCsvLogPath = "CsvLogPath";
        public const string kConfigUartLogPath = "UartLogPath";
        public const string kConfigTestFlowPath = "TestFlowPath";
        public const string kConfigSfcServer = "SfcServer";
        public const string kConfigStationName = "StationName";
        public const string kConfigLineName = "LineName";
        public const string kConfigScanBarcode = "ScanBarCode?";
        public const string kConfigUploadData = "UploadData?";

        public const string kAutMode = "AutoMode";
        public const string kManualMode = "ManualMode";

        public const string kUUT0TestStaus = "UUT0Status";
        public const string kUUT1TestStaus = "UUT1Status";
        public const string kUUT0Errcode = "UUT0Errcode";
        public const string kUUT1TErrcode ="UUT1Errcode";
        public const string kLoopModeBase = "LoopMode";

        //Configuration add by lxl
        public const string kFixtureCom  = "MCUCOM";
        public const string kFixtureConfig = "MCUConfig";

        public const string kArm1Com = "ArmCom1";
        public const string kArm2Com = "ArmCom2";
        public const string kArm3Com = "ArmCom3";
        public const string kArm4Com = "ArmCom4";
        public const string kArm5Com = "ArmCom5";
        public const string kArm6Com = "ArmCom6";

        public const string kArm1Config = "Arm1Config";
        public const string kArm2Config = "Arm2Config";
        public const string kArm3Config = "Arm3Config";
        public const string kArm4Config = "Arm4Config";
        public const string kArm5Config = "Arm5Config";
        public const string kArm6Config = "Arm6Config";

        public const string kDUT1Com = "DUTCom1";
        public const string kDUT2Com = "DUTCom2";
        public const string kDUT3Com = "DUTCom3";
        public const string kDUT4Com = "DUTCom4";
        public const string kDUT5Com = "DUTCom5";
        public const string kDUT6Com = "DUTCom6";

        public const string kDUT1Config = "DUT1Config";
        public const string kDUT2Config = "DUT2Config";
        public const string kDUT3Config = "DUT3Config";
        public const string kDUT4Config = "DUT4Config";
        public const string kDUT5Config = "DUT5Config";
        public const string kDUT6Config = "DUT6Config";

        public const string kScannerCom = "ScannerCom";
        public const string kScannerConfig = "ScannerConfig";

        public const string kLEDCom = "LEDCom";
        public const string kLEDConfig = "LEDConfig";

        public const string kLED2Com = "LED2Com";
        public const string kLED2Config = "LED2Config";

        public const string kLuxMeterCom = "LuxMeterCom";
        public const string kLuxMeterConfig = "LuxMeterConfig";

        public const string kSensor1Com = "SensorLCom";
        public const string kSensor1Config = "Sensor1Config";

        public const string kSensor2Com = "SensorRCom";
        public const string kSensor2Config = "SensorRConfig";
        //global configuration
        public const string kGlobalAppDir = "AppDir";
        //Context
        public const string kContextID = "uid";
        public const string kContextStartTime = "StartTime";
        public const string kContextTestTime = "TestTime";
        public const string kContextEndTime = "EndTime";
        public const string kContextMLBSN = "MLB_SN";
        public const string kContextCFG = "CFG";

        public const string kContextSN1 = "SN1";
        public const string kContextSN2 = "SN2";
        public const string kContextSN3 = "SN3";
        public const string kContextSN4 = "SN4";
        public const string kContextSN5 = "SN5";
        public const string kContextSN6 = "SN6";
        public const string kContextSN7 = "SN7";
        public const string kContextSN8 = "SN8";

        public const string kDutType = "DUTTYPE";
        public const string kDevId = "DEVID";
        public const string kAppVerSion ="APPVERSION";
        public const string kStationName = "STATIONM";
        public const string kStationType = "STATIONTYPE";
        public const string kProductName = "PRODUCTNM";

        public const string kCusTomer = "CUSTOMER";
        public const string kWorkOrder = "WORKORDER";
        public const string kIsAdminLogin = "isadminlogin";

        public const string kDataBase="DATABASENM";
        public const string kDataBaseIP ="DATABASEIP";
        public const string kProjectNm="PROJECTNM";

        public const string kStarttime = "STARTTESTTM";
        public const string kTimeStamp = "TIMESTAMP";

        public const string kProjectType = "PROJECTTYPE";
        public const string kStatinNum = "STATIONNUM";
        public const string kAppBuilderTime = "BUILDERTIME";
        public const string kXid = "XID";
        public const string SfcPath = "SFCPATH";

        public const string kDueTimeALSC1 = "Alsc1DueTime";
        public const string kDueTimeALSC2 = "Alsc2DueTime";
       
    }



    public static class tmGlobalobj
    {

        public static object objLockConfig = new object();

        public static bool Testing=false;

        public static int PassCount=0;
        public static int Failcount=0;
    
    }






    public static class tmEnvironment
    {
        public static string AppDir()
        {
            return System.AppDomain.CurrentDomain.BaseDirectory;//System.Windows.Forms.Application.ExecutablePath;;
        }

        public static void SystemLog(string strlog)
        {
        }
        public static void SystemNotification(string msg)
        {

        }
        public static void msgbox(string msg)
        {
        }

        public static tmTreeNode LoadProfile(string file)
        {
            tmTreeNode tree = new tmTreeNode("Root");   //root
            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            doc.Load(file);
            System.Xml.XmlNodeList nodeList = doc.GetElementsByTagName("items");
            if (nodeList.Count==0)
            {
                throw new System.Exception("Invalid profile,couldn't find \"items\" element.");
            }
            System.Xml.XmlElement element = nodeList[0] as System.Xml.XmlElement;
            if (element==null)
            {
                throw new System.Exception("Invalid profile,item is not a element.");
            }
            nodeList = element.GetElementsByTagName("item");
            foreach (System.Xml.XmlNode n in nodeList)
            {
                element = n as System.Xml.XmlElement;
                TestItem aItem = new TestItem();
                foreach (System.Xml.XmlAttribute a in element.Attributes)
                {
                    aItem[a.Name] = a.InnerText;
                }
                tmTreeNode keyNode = new tmTreeNode(aItem);
                tree.AddChildNode(keyNode);   //add group item

                //List child item
                System.Xml.XmlNodeList childList = element.GetElementsByTagName("subitem");
                foreach (System.Xml.XmlNode c in childList)
                {
                    element = c as System.Xml.XmlElement;
                    TestItem child = new TestItem();
                    foreach (System.Xml.XmlAttribute a in element.Attributes)
                    {
                        child[a.Name] = a.InnerText;
                    }
                    keyNode.AddChildNode(new tmTreeNode(child));
                }
            }
            return tree;
        }

        static void ParseLuaTable(tmTreeNode tree,LuaInterface.LuaTable table)
        {
            try
            {
                foreach (object index in table.Keys)
                {
                    LuaInterface.LuaTable luaItem = table[index] as LuaInterface.LuaTable;
                    if (null == luaItem) continue;
                    TestItem aItem = new TestItem();
                    //Console.WriteLine("LuaInterface.LuaTable<{0}> <{1}>", index as string,luaItem.ToString());
                    foreach (object key in luaItem.Keys)
                    {
                        if (null == luaItem[key]) continue;
                        //error occur here.
                        //Console.WriteLine("LuaInterface.luaItem[{0}],[{1}]", key as string, luaItem[key].ToString());
                        aItem[key.ToString()] = luaItem[key].ToString();
                    }
                    tmTreeNode node = new tmTreeNode(aItem);

                    tree.AddChildNode(node);

                    LuaInterface.LuaTable sub = luaItem["sub"] as LuaInterface.LuaTable;

                    if (null != sub)
                    {
                        ParseLuaTable(node, sub);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message + "In ParseLuaTable");
            }
        }

        public static tmTreeNode ParseProfile(Engine engine)
        {
            ScriptEngine se = engine.GetScriptEngine(0) as ScriptEngine;
            LuaInterface.Lua lua = se.GetScriptHandle() as LuaInterface.Lua;
            LuaInterface.LuaTable table = lua["items"] as LuaInterface.LuaTable;
            if (table == null)
            {
                throw new System.Exception("Invalid profile,could't found the global variabl \"items\"");
            }

            tmTreeNode tree = new tmTreeNode("Root");   //root

            ParseLuaTable(tree, table);

            return tree;
        }
        public static string ParseModule(Engine engine)
        {
            ScriptEngine se = engine.GetScriptEngine(0) as ScriptEngine;
            LuaInterface.Lua lua = se.GetScriptHandle() as LuaInterface.Lua;
            string str = (string)lua["Module"];
            return str;
        }
        public static string ParseVersion(Engine engine)
        {
            ScriptEngine se = engine.GetScriptEngine(0) as ScriptEngine;
            LuaInterface.Lua lua = se.GetScriptHandle() as LuaInterface.Lua;
            string str = (string)lua["Version"];
            return str;
        }
    }

    /// <summary>
    /// 时间相关函数
    /// </summary>
    public static class GT_Function
    {
        /// <summary>
        /// 将Unix时间戳转换为DateTime类型时间
        /// </summary>
        /// <param name="d">double 型数字</param>
        /// <returns>DateTime</returns>
        public static System.DateTime ConvertIntDateTime(double d)
        {
            System.DateTime time = System.DateTime.MinValue;
            System.DateTime startTime = System.TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            time = startTime.AddSeconds(d);
            return time;
        }
        /// <summary>
        /// 将c# DateTime时间格式转换为Unix时间戳格式
        /// </summary>
        /// <param name="time">时间</param>
        /// <returns>double</returns>
        public static double ConvertDateTimeInt(System.DateTime time)
        {
            double intResult = 0;
            System.DateTime startTime = System.TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            intResult = (time - startTime).TotalSeconds;
            return intResult;
        }
    }
}
