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
using Server;
using MQTT;
using System.Net.NetworkInformation;

namespace ServerApp
{
	public class Program
	{
		public static System.Timers.Timer timer1 = new System.Timers.Timer(40000);
		public static MQTT.mqtt reciever = new MQTT.mqtt();
		public static bool[] serverArray = new bool[3];
		static async Task Main(string[] args)
		{
			

			
			timer1.Elapsed += Timer1Elapsed;
			timer1.Start();

			reciever.MessageReceived += (sender, message) =>
			{
				Console.WriteLine("Received message: " + message);
				int point = int.Parse(message);
				serverArray[point - 1] = true;

			};

			await reciever.Handle_Received_Application_Message();
		}

		private static async void Timer1Elapsed(object sender, ElapsedEventArgs e)
		{
			for (int i = 0; i < serverArray.Length; i++)
			{
				if (serverArray[i])
				{
					Console.WriteLine($"Server {i + 1} online");
				}
				else
				{
					Console.WriteLine($"Server {i + 1} offline");
				}
			}

			for (int i = 0; i < serverArray.Length; i++)
			{
				serverArray[i] = false;
			}
		}
	}
}

