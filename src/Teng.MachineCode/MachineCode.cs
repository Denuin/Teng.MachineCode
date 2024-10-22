// MachineCode.cs
// 机器码生成类
//
// 作者: Denuin
// 创建日期: 2024-10-22
// Github: https://github.com/Denuin/Teng.MachineCode
// 修改记录:
// - 2024-10-22 - 初始创建
//

using System.Management;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Teng
{
    /// <summary>
    /// 机器码
    /// </summary>
    public sealed class MachineCode
    {
        /// <summary>
        /// 盐
        /// </summary>
        public static string UseSalt { get; set; } = "MachineCode";

        /// <summary>
        /// CPU信息
        /// </summary>
        public static bool UseCpuInfo { get; set; } = true;

        /// <summary>
        /// 硬盘ID
        /// </summary>
        public static bool UseHDid { get; set; } = true;

        /// <summary>
        /// 硬盘序列号
        /// </summary>
        public static bool UseDiskSerialNumber { get; set; } = true;

        /// <summary>
        /// 网卡MAC地址
        /// </summary>
        public static bool UseMacAddress { get; set; } = true;

        /// <summary>
        /// BIOS序列号
        /// </summary>
        public static bool UseBiosSerialNumber { get; set; } = true;

        private static readonly Lazy<string> _lazyInstance = new(() =>
        {
            var machineCode = new MachineCode();
            string mc = machineCode.GetMachineCodeString();
            byte[] bytes = SHA1.HashData(Encoding.UTF8.GetBytes(mc));
            string sha1Code = string.Join("", bytes.Select(a => a.ToString("X2")));
            return Regex.Replace(sha1Code, @"\w{8}", "$0-", RegexOptions.IgnoreCase).TrimEnd('-');
        });

        /// <summary>
        /// 机器码实例
        /// </summary>
        public static string Instance => _lazyInstance.Value;

        private MachineCode()
        { }

        /// <summary>
        /// 机器码原文
        /// </summary>
        /// <returns></returns>
        public string GetMachineCodeString()
        {
            List<string> lsta = [UseSalt];
            if (UseCpuInfo)
            {
                lsta.Add($"[CpuInfo]{GetCpuInfo()}");
            }
            if (UseHDid)
            {
                lsta.Add($"[HDid]{GetHDid()}");
            }
            if (UseDiskSerialNumber)
            {
                lsta.Add($"[DiskSerialNumbe]{GetDiskSerialNumber()}");
            }
            if (UseMacAddress)
            {
                lsta.Add($"[MAC]{GetMacAddress()}");
            }
            if (UseBiosSerialNumber)
            {
                lsta.Add($"[BiosSerialNumber]{GetBiosSerialNumber()}");
            }
            return string.Join(".", lsta);
        }

        /// <summary>
        /// 获取cpu序列号
        /// </summary>
        public string GetCpuInfo()
        {
            return GetManagementObjectValue("Win32_Processor", "ProcessorId");
        }

        /// <summary>
        /// 获取硬盘ID
        /// </summary>
        public string GetHDid()
        {
            return GetManagementObjectValue("Win32_DiskDrive", "Model");
        }

        /// <summary>
        /// 获得硬盘序列号
        /// </summary>
        public string GetDiskSerialNumber()
        {
            return GetManagementObjectValue("Win32_PhysicalMedia", "SerialNumber");
        }

        /// <summary>
        /// 获取网卡硬件地址
        /// </summary>
        public string GetMacAddress()
        {
            string? result = "";
            try
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    using ManagementClass mc = new("Win32_NetworkAdapterConfiguration");
                    List<string> lsta = [];
                    ManagementObjectCollection moc2 = mc.GetInstances();
                    foreach (ManagementObject mo in moc2)
                    {
                        if ((bool)mo["IPEnabled"] && mo["MacAddress"] != null)
                        {
                            var mac = mo["MacAddress"]?.ToString()?.Trim();
                            if (!string.IsNullOrEmpty(mac))
                            {
                                lsta.Add(mac);
                            }
                        }
                        mo.Dispose();
                    }
                    if (lsta.Count > 0)
                    {
                        result = string.Join(",", lsta);
                    }
                }
            }
            catch
            {
            }
            return result ?? "unknow";
        }

        /// <summary>
        /// 获得主板序列号
        /// </summary>
        public string GetBiosSerialNumber()
        {
            return GetManagementObjectValue("Win32_BaseBoard", "SerialNumber");
        }

        #region 私有方法

        private static string GetManagementObjectValue(string path, string propertyKey, string defaultValue = "unknow")
        {
            string? result = null;
            try
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    using ManagementClass cimobject = new(path);
                    List<string> lsta = [];
                    foreach (ManagementObject mo in cimobject.GetInstances())
                    {
                        if (mo == null || mo.Properties == null)
                        {
                            continue;
                        }
                        var info = mo.Properties[propertyKey]?.Value?.ToString()?.Trim();
                        if (!string.IsNullOrEmpty(info))
                        {
                            lsta.Add(info);
                        }
                        mo?.Dispose();
                    }
                    if (lsta.Count > 0)
                    {
                        result = string.Join(",", lsta);
                    }
                }
            }
            catch
            {
            }
            return result ?? defaultValue;
        }

        #endregion 私有方法
    }
}