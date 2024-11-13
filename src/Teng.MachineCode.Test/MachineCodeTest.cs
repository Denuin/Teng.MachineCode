using NUnit.Framework;
using System;

namespace Teng.Test
{
    public class MachineCodeTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestCtor()
        {
            var mc = new MachineCode();
            // 输出原文信息
            Console.WriteLine($"机器码：{mc.GetMachineCodeString()}");
            Console.WriteLine($"CPU序列号：{mc.GetCpuInfo()}");
            Console.WriteLine($"硬盘ID：{mc.GetHDid()}");
            Console.WriteLine($"硬盘序列号：{mc.GetDiskSerialNumber()}");
            Console.WriteLine($"网卡硬件地址：{mc.GetMacAddress()}");
            Console.WriteLine($"主板序列号：{mc.GetBiosSerialNumber()}");
        }
    }
}