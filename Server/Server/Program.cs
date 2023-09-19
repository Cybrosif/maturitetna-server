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
using MQTT;


namespace ServerApp
{
	public class Program
	{
	
		static async Task Main(string[] args)
		{
			MQTT.mqtt mqtt = new MQTT.mqtt();
			

			System.Timers.Timer timer1 = new System.Timers.Timer(40000);
			timer1.Elapsed += Timer1Elapsed;
			timer1.Start();

			await mqtt.Handle_Received_Application_Message();
		}

		private static async void Timer1Elapsed(object sender, ElapsedEventArgs e)
		{
			
		}
	}
}

