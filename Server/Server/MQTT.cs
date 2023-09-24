using System;
using System.Text;
using System.Threading.Tasks;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Protocol;
using Newtonsoft.Json;
using Server;

namespace MQTT
{
	public class mqtt
	{
		// Define an event to signal that a message has been received.
		public event EventHandler<string> MessageReceived;

		public async Task Handle_Received_Application_Message()
		{
			var mqttFactory = new MqttFactory();
			using (var mqttClient = mqttFactory.CreateMqttClient())
			{
				var mqttClientOptions = new MqttClientOptionsBuilder()
					.WithTcpServer("91.121.93.94", 1883)
					.Build();

				await mqttClient.ConnectAsync(mqttClientOptions, CancellationToken.None);

				var mqttSubscribeOptions = mqttFactory.CreateSubscribeOptionsBuilder()
					.WithTopicFilter(f =>
					{
						f.WithQualityOfServiceLevel(MqttQualityOfServiceLevel.AtLeastOnce);
						f.WithTopic("test/test123/onlinecheck");
					})
					.Build();

				mqttClient.ApplicationMessageReceivedAsync += e =>
				{
					string json = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
					var response = JsonConvert.DeserializeObject<Configuration>(json);
					string serverID = response.serverID;
					// Raise the MessageReceived event and pass the message.
					MessageReceived?.Invoke(this, serverID);

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
