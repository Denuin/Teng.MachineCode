# Teng.MachineCode

机器码生成类

## 使用方法

### 1. 快速使用
```cs
using Teng;

void Main()
{
	// 配置选项
	MachineCode.UseSalt = "<自定义内容>"; // 默认为“MachineCode”
	MachineCode.UseCpuInfo = true; // 是否使用CPU信息，默认为true
	MachineCode.UseHDid = true; // 是否使用硬盘ID，默认为true
	MachineCode.UseDiskSerialNumber = true; // 是否使用硬盘序列号，默认为true
	MachineCode.UseMacAddress = true; // 是否使用网卡MAC地址，默认为true
	MachineCode.UseBiosSerialNumber = true; // 是否使用BIOS序列号，默认为true

	MachineCode.OptionHardDiskCount = 1; // 设置硬盘数，默认为1（使用一个硬盘）

	// 获取机器码
	Console.WriteLine($"机器码：{MachineCode.Instance}");
	
	// ... 
}
```

### 2. 进阶使用
```cs
using Teng;

void Main()
{
	// 配置选项
	// ...

	var mc = new MachineCode();
	// 输出原文信息
	Console.WriteLine($"机器码：{mc.GetMachineCodeString()}");
	Console.WriteLine($"CPU序列号：{mc.GetCpuInfo()}");
	Console.WriteLine($"硬盘ID：{mc.GetHDid()}");
	Console.WriteLine($"硬盘序列号：{mc.GetDiskSerialNumber()}");
	Console.WriteLine($"网卡硬件地址：{mc.GetMacAddress()}");
	Console.WriteLine($"主板序列号：{mc.GetBiosSerialNumber()}");
	
	// ...
}
```

## 版本日志
 - 1.0.2 (2024-11-13)
    - Add OptionHardDiskCount
	- bug fix
 - 1.0.1 (2024-10-31)
    - Platform support : net462, net481, net6.0, net8.0
 - 1.0.0 (2024-10-22)
    - First release

> https://github.com/Denuin/Teng.MachineCode