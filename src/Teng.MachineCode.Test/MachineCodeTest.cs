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
            // ���ԭ����Ϣ
            Console.WriteLine($"�����룺{mc.GetMachineCodeString()}");
            Console.WriteLine($"CPU���кţ�{mc.GetCpuInfo()}");
            Console.WriteLine($"Ӳ��ID��{mc.GetHDid()}");
            Console.WriteLine($"Ӳ�����кţ�{mc.GetDiskSerialNumber()}");
            Console.WriteLine($"����Ӳ����ַ��{mc.GetMacAddress()}");
            Console.WriteLine($"�������кţ�{mc.GetBiosSerialNumber()}");
        }
    }
}