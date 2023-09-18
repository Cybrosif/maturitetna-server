using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemInfo
{
	public class SystemInfoData
	{
		public double CpuUsage { get; set; }
		public ulong TotalMemoryMB { get; set; }
		public ulong FreeMemoryMB { get; set; }
		public ulong UsedMemoryMB { get; set; }
		public string FileSystem { get; set; }
		public long TotalSizeMB { get; set; }
		public long AvailableFreeSpaceMB { get; set; }
		public TimeSpan Uptime { get; set; }
	}
}
