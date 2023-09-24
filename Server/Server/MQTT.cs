using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Protocol;
using SystemInfo;
using Newtonsoft.Json;

namespace MQTT
{
	public class mqtt
	{
		public static async Task Handle_Received_Application_Message()
		{
			var mqttFactory = new MqttFactory();
			using (var mqttClient = mqttFactory.CreateMqttClient())
			{
				var mqttClientOptions = new MqttClientOptionsBuilder().WithTcpServer("91.121.93.94", 1883).Build();

				await mqttClient.ConnectAsync(mqttClientOptions, CancellationToken.None);

				var mqttSubscribeOptions = mqttFactory.CreateSubscribeOptionsBuilder()
					.WithTopicFilter(
						f =>
						{
							//f.WithQualityOfServiceLevel(MqttQualityOfServiceLevel.AtLeastOnce);
							f.WithTopic("test/test123");
						})
					.Build();
 
				mqttClient.ApplicationMessageReceivedAsync += e =>
				{
					string json = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);

					// Deserialize JSON data back into SystemInfoData
					var systemInfoData = JsonConvert.DeserializeObject<SystemInfoData>(json);

					// Access individual variables
					double cpuUsage = systemInfoData.CpuUsage;
					ulong totalMemoryMB = systemInfoData.TotalMemoryMB;
					ulong freeMemoryMB = systemInfoData.FreeMemoryMB;
					ulong usedMemoryMB = systemInfoData.UsedMemoryMB;
					string fileSystem = systemInfoData.FileSystem;
					long totalSizeMB = systemInfoData.TotalSizeMB;
					long availableFreeSpaceMB = systemInfoData.AvailableFreeSpaceMB;
					TimeSpan uptime = systemInfoData.Uptime;

					// Use the parsed variables as needed
					Console.WriteLine($"CPU Usage: {cpuUsage:F2}%");
					Console.WriteLine($"Total Memory: {totalMemoryMB} MB");
					Console.WriteLine($"Free Memory: {freeMemoryMB} MB");
					Console.WriteLine($"Used Memory: {usedMemoryMB} MB");
					Console.WriteLine($"File System: {fileSystem}");
					Console.WriteLine($"Total Size: {totalSizeMB} MB");
					Console.WriteLine($"Available Free Space: {availableFreeSpaceMB} MB");
					Console.WriteLine($"Uptime: {uptime}");
					Console.WriteLine();

					return Task.CompletedTask;
				};

				await mqttClient.SubscribeAsync(mqttSubscribeOptions, CancellationToken.None);

				Console.WriteLine("MQTT client subscribed to topic.");

				Console.WriteLine("Press enter to exit.");
				Console.ReadLine();
			}
		}
	}
}
