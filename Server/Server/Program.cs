using System;
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
			await mqtt.Handle_Received_Application_Message();
		}
	}
}

