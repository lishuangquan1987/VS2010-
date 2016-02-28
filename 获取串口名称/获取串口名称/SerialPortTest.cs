using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace 获取串口名称
{
    public class HardWareOperation
    {
        /// <summary>
        /// 设备的各项属性，注意有些属性是不通用的，例如SPDRP_FRIENDLYNAME只适用于端口设备
        /// </summary>
        public enum SPDRP_
        {
            SPDRP_DEVICEDESC = (0x00000000),  // DeviceDesc (R/W)
            SPDRP_HARDWAREID = (0x00000001),  // HardwareID (R/W)
            SPDRP_SERVICE = (0x00000004), // Service (R/W)
            SPDRP_CLASS = (0x00000007),  // Class (R--tied to ClassGUID)
            SPDRP_CLASSGUID = (0x00000008),  // ClassGUID (R/W)
            SPDRP_DRIVER = (0x00000009),  // Driver (R/W)
            SPDRP_CONFIGFLAGS = (0x0000000A), // ConfigFlags (R/W)
            SPDRP_MFG = (0x0000000B), // Mfg (R/W)
            SPDRP_FRIENDLYNAME = (0x0000000C),  // FriendlyName (R/W)
            SPDRP_PHYSICAL_DEVICE_OBJECT_NAME = (0x0000000E),  // PhysicalDeviceObjectName (R)
            SPDRP_CAPABILITIES = (0x0000000F), // Capabilities (R)
            SPDRP_REMOVAL_POLICY_HW_DEFAULT = (0x00000020),  // Hardware Removal Policy (R)
            SPDRP_INSTALL_STATE = (0x00000022), // Device Install State (R)
        }
        public const int DIGCF_ALLCLASSES = (0x00000004);
        public const int DIGCF_DEVICEINTERFACE = 0x00000010;
        public const int DIGCF_PRESENT = (0x00000002);
        public const int INVALID_HANDLE_VALUE = -1;
        public const int MAX_DEV_LEN = 1000;

        /// <summary>
        /// 获取一个指定类别或全部类别的所有已安装设备的信息
        /// </summary>
        /// <param name="gClass">该类别对应的guid</param>
        /// <param name="iEnumerator">类别名称（在HKLMSYSTEMCurrentControlSetEnum内获取）</param>
        /// <param name="hParent">应用程序定义的窗口句柄</param>
        /// <param name="nFlags">获取的模式</param>
        /// <returns>设备信息集合的句柄</returns>
        [DllImport("setupapi.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern IntPtr SetupDiGetClassDevs(ref Guid gClass, String enumerator, IntPtr hParent, UInt32 nFlags);

        /// <summary>
        /// 获得该设备的设备范例ID
        /// </summary>
        /// <param name="DeviceInfoSet">设备信息集合</param>
        /// <param name="DeviceInfoData">表示该设备</param>
        /// <param name="DeviceInstanceId">设备范例ID（输出）</param>
        /// <param name="DeviceInstanceIdSize">该ID所占大小（字节）</param>
        /// <param name="RequiredSize">需要多少字节</param>
        /// <returns>是否成功</returns>
        [DllImport("setupapi.dll", SetLastError = true)]
        public static extern bool SetupDiGetDeviceInstanceId(IntPtr DeviceInfoSet,
                                    HardWareOperation.SP_DEVINFO_DATA DeviceInfoData,
                                    StringBuilder DeviceInstanceId,
                                    UInt32 DeviceInstanceIdSize,
                                    UInt32 RequiredSize);

        /// <summary>
        /// 枚举指定设备信息集合的成员，并将数据放在SP_DEVINFO_DATA中
        /// </summary>
        /// <param name="lpInfoSet">设备信息集合句柄</param>
        /// <param name="dwIndex">元素索引</param>
        /// <param name="devInfoData">表示一个设备（作为输出）</param>
        /// <returns>是否成功</returns>
        [DllImport("setupapi.dll", SetLastError = true)]
        public static extern bool SetupDiEnumDeviceInfo(IntPtr lpInfoSet, UInt32 dwIndex, SP_DEVINFO_DATA devInfoData);

        /// <summary>
        /// 获取指定设备的属性
        /// </summary>
        /// <param name="lpInfoSet">设备信息集合</param>
        /// <param name="DeviceInfoData">表示该设备</param>
        /// <param name="Property">表示要获取哪项属性</param>
        /// <param name="PropertyRegDataType">注册类型</param>
        /// <param name="PropertyBuffer">属性（输出）</param>
        /// <param name="PropertyBufferSize">存储属性的字节大小</param>
        /// <param name="RequiredSize">需要的字节大小</param>
        /// <returns>是否成功</returns>
        [DllImport("setupapi.dll", SetLastError = true)]
        public static extern bool SetupDiGetDeviceRegistryProperty(IntPtr lpInfoSet, 
                                    SP_DEVINFO_DATA DeviceInfoData, 
                                    UInt32 Property, 
                                    UInt32 PropertyRegDataType, 
                                    StringBuilder PropertyBuffer, 
                                    UInt32 PropertyBufferSize, 
                                    IntPtr RequiredSize);

        /// <summary>
        /// 销毁一个设备信息集合，并且释放所有关联的内存
        /// </summary>
        /// <param name="lpInfoSet">设备信息集合</param>
        /// <returns></returns>
        [DllImport("setupapi.dll", SetLastError = true)]
        public static extern bool SetupDiDestroyDeviceInfoList(IntPtr lpInfoSet);

        
        /// <summary>
        /// 设备信息数据
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public class SP_DEVINFO_DATA
        {
            public int cbSize;//本结构的大小（字节表示）
            public Guid classGuid;//本结构所表示的设备的GUID
            public int devInst;//设备句柄
            public ulong reserved;//没用
        }
        //一个调用方法GetHardWareIdFromPortName

        /// <summary>
        /// 通过串口号获得对应设备的硬件ID（里面包括该串口设备的vid和pid）
        /// </summary>
        /// <param name="portName">串口号</param>
        /// <returns></returns>
        public string GetHardWareIdFromPortName(string portName)
        {
            //先判断当前机器是否有该串口号
            string[] ports = System.IO.Ports.SerialPort.GetPortNames();
            int k;
            //if (!DataOperator.IsContainElement(ports, portName,out k)) return null;
            Guid myGUID = Guid.Empty;
            string enumerator = "USB";
            try
            {
                IntPtr hDevInfo = HardWareOperation.SetupDiGetClassDevs(ref myGUID, enumerator, IntPtr.Zero, HardWareOperation.DIGCF_ALLCLASSES | HardWareOperation.DIGCF_PRESENT);
                if (hDevInfo.ToInt32() == HardWareOperation.INVALID_HANDLE_VALUE)
                {
                    throw new Exception("没有该类设备");
                }
                HardWareOperation.SP_DEVINFO_DATA deviceInfoData;//想避免在api中使用ref，就把structure映射成类
                deviceInfoData = new HardWareOperation.SP_DEVINFO_DATA();
                deviceInfoData.cbSize = 28;//如果要使用SP_DEVINFO_DATA，一定要给该项赋值28=16+4+4+4
                deviceInfoData.devInst = 0;
                deviceInfoData.classGuid = System.Guid.Empty;
                deviceInfoData.reserved = 0;
                UInt32 i;
                StringBuilder property = new StringBuilder(HardWareOperation.MAX_DEV_LEN);
                for (i = 0; HardWareOperation.SetupDiEnumDeviceInfo(hDevInfo, i, deviceInfoData); i++)
                {
                    //       Console.Write(deviceInfoData.classGuid.ToString());
                    //       HardWareOperation.SetupDiGetDeviceInstanceId(hDevInfo, deviceInfoData, porperty, (uint)porperty.Capacity, 0);
                    HardWareOperation.SetupDiGetDeviceRegistryProperty(hDevInfo, deviceInfoData,
                        (uint)HardWareOperation.SPDRP_.SPDRP_CLASS,
                        0, property, (uint)property.Capacity, IntPtr.Zero);
                    if (property.ToString().ToLower() != "ports") continue;//首先看看是不是串口设备（有些USB设备不是串口设备）
                    HardWareOperation.SetupDiGetDeviceRegistryProperty(hDevInfo, deviceInfoData,
                        (uint)HardWareOperation.SPDRP_.SPDRP_FRIENDLYNAME,
                        0, property, (uint)property.Capacity, IntPtr.Zero);
                    if (!property.ToString().ToLower().Contains(portName.ToLower())) continue;//找到对应于portName的设备
                    HardWareOperation.SetupDiGetDeviceRegistryProperty(hDevInfo, deviceInfoData,
                        (uint)HardWareOperation.SPDRP_.SPDRP_HARDWAREID,
                        0, property, (uint)property.Capacity, IntPtr.Zero);
                    break;

                }
                HardWareOperation.SetupDiDestroyDeviceInfoList(hDevInfo);
                return property.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }

   }

    }
}


 

