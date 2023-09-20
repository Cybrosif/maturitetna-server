using System;
using System.Timers;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Linq;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Protocol;
using System.Text;
using Newtonsoft.Json;
using SystemInfo;
//using MQTT;


namespace ServerApp
{
	public class Program
	{
		public static System.Timers.Timer timer1 = new System.Timers.Timer(40000);
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
							f.WithQualityOfServiceLevel(MqttQualityOfServiceLevel.AtLeastOnce);
							f.WithTopic("test/test123/onlinecheck");
						})
					.Build();

				mqttClient.ApplicationMessageReceivedAsync += e =>
				{
					string json = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);

					MessageRecieved();

					return Task.CompletedTask;
				};

				await mqttClient.SubscribeAsync(mqttSubscribeOptions, CancellationToken.None);

				Console.WriteLine("MQTT client subscribed to topic.");

				Console.WriteLine("Press enter to exit.");
				Console.ReadLine();
			}
		}

		static async Task Main(string[] args)
		{
			

			
			timer1.Elapsed += Timer1Elapsed;
			timer1.Start();


			await Handle_Received_Application_Message();
		}

		private static async void Timer1Elapsed(object sender, ElapsedEventArgs e)
		{
			Console.WriteLine("server dol padu :(");
		}

		public static void MessageRecieved()
		{
			Console.WriteLine("dobu");
			timer1.Stop();
			timer1.Start();
		}
	}
}

