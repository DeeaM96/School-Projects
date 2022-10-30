using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Windows.Controls;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using System.Windows;

namespace LOG430_VI
{
    public class MqttApi
    {
        public MqttClient client { get; set; }

        private ListView mqttOutput;
        private Dispatcher viDispatcher;

        /// <summary>
        /// Constructeur de l'API MQTT
        /// et connexion au service d'informations trafic en temps réel
        /// </summary>
        /// <param name="viDispatcher"></param>
        /// <param name="mqttOutput"></param>
        public MqttApi(Dispatcher viDispatcher, ListView mqttOutput)
        {
            this.viDispatcher = viDispatcher;
            this.mqttOutput = mqttOutput;
            ConnectMqtt();
        }

        /// <summary>
        /// Connexion au service d'informations trafic en temps réel
        /// </summary>
        public void ConnectMqtt()
        {
            client = new MqttClient("mqtt.cgmu.io");

            string clientId = Guid.NewGuid().ToString();
            client.Connect(clientId);

            if (client.IsConnected)
            {
                InitializeClient();
            }
            else
            {
                MessageBox.Show("You were unable to connect to real-time traffic information for the city of Montreal.",
                    "Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        /// <summary>
        /// Initialisation du client MQTT
        /// </summary>
        private void InitializeClient()
        {
            client.MqttMsgPublishReceived += Client_MqttMsgPublishReceived;
            client.ConnectionClosed += Client_ConnectionClosed;

            MessageBox.Show("You are connected to real-time traffic information for the city of Montreal.",
                "Welcome!", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        /// <summary>
        /// Retour des messages publiés des Topics actif
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            viDispatcher.Invoke(() =>
            {
                mqttOutput.Items.Insert(0, new MqttOutput(e.Topic, Encoding.UTF8.GetString(e.Message)));
            });
        }

        /// <summary>
        /// Est soulevé si l'utilisateur est déconnecté du sa connexion MQTT
        /// et tente de reconnecter celui-ci
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Client_ConnectionClosed(object sender, EventArgs e)
        {
            MessageBox.Show("You were disconnected from real-time traffic information for the city of Montreal." +
                "\nAttempting to reconnect...", "Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);

            do
            {
                ConnectMqtt();
            } while (!client.IsConnected);
        }

        /// <summary>
        /// S'abonne a un Topic
        /// </summary>
        /// <param name="topic"></param>
        public void AddTopic(string topic)
        {
            client.Subscribe(new string[] { topic }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
        }

        /// <summary>
        /// Se désabonne d'un ou plusieurs Topic(s)
        /// </summary>
        /// <param name="topics"></param>
        public void RemoveTopics(string[] topics)
        {
            client.Unsubscribe(topics);
        }

        /// <summary>
        /// Déconnecte le client MQTT
        /// </summary>
        public void DisconnectClient()
        {
            if (client != null)
            {
                client.Disconnect();
            }
        }
    }
}
