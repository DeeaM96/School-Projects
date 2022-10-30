using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace LOG430_VI
{
    /// <summary>
    /// Interaction logic for VI.xaml
    /// </summary>
    public partial class VI : Window
    {
        private MqttApi mqttApi;

        /// <summary>
        /// Initialisation de la connexion MQTT au service d'informations trafic en temps réel
        /// </summary>
        public VI()
        {
            InitializeComponent();

            mqttApi = new MqttApi(Dispatcher, mqttOutput);

            // Abonnement a des Topics généraux
            if (mqttApi.client.IsConnected)
            {
                AddTopic("worldcongress2017/pilot_resologi/odtf1/ca/qc/mtl/mobil/infra/gateway/ipc0/gat-00000-01/heartbeat");
                //AddTopic("worldcongress2017/pilot_resologi/odtf1/ca/qc/mtl/#");
                AddTopic("worldcongress2017/pilot_resologi/odtf1/ca/qc/mtl/mobil/traf/detector/det0/det-00721-01/lane0/measure0/car/avg-spd");
                AddTopic("worldcongress2017/pilot_resologi/odtf1/ca/qc/mtl/mobil/traf/detector/det1/det-00773-02/zone2/class1/vehicle-speed");
            }
        }

        /// <summary>
        /// Déconnexion du MQTT et quitte l'application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closed(object sender, EventArgs e)
        {
            mqttApi.DisconnectClient();

            Environment.Exit(0);
        }

        /// <summary>
        /// Envoie en paramètre le contenue du TextBox à ajouter comme Topic
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddTopic_Click(object sender, RoutedEventArgs e)
        {
            if (addTopicTxtBox.Text.Length > 0)
            {
                AddTopic(addTopicTxtBox.Text);
            }
        }

        /// <summary>
        /// Ajoute le Topic à la liste de Topic actif 
        /// et envoie le Topic au client MQTT pour s'y abonner
        /// </summary>
        /// <param name="topic"></param>
        private void AddTopic(string topic)
        {
            mqttApi.AddTopic(topic);
            activeTopics.Items.Add(topic);
        }

        /// <summary>
        /// Envoie en paramètre les Topics actif sélectionné du ListBox pour les supprimer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RemoveTopic_Click(object sender, RoutedEventArgs e)
        {
            if (activeTopics.SelectedItems.Count > 0)
            {
                string[] selectedTopics = new string[activeTopics.SelectedItems.Count];
                activeTopics.SelectedItems.CopyTo(selectedTopics, 0);

                RemoveTopics(selectedTopics);
            }
        }

        /// <summary>
        /// Supprime les Topics de la liste de Topic actif 
        /// et envoie les Topics au client MQTT pour s'y désabonner
        /// </summary>
        /// <param name="topics"></param>
        private void RemoveTopics(string[] topics)
        {
            mqttApi.RemoveTopics(topics);
            foreach (string topic in topics)
            {
                activeTopics.Items.Remove(topic);
            }
        }

        private void AggregateTopic_Click(object sender, RoutedEventArgs e)
        {
            if (activeTopics.SelectedItems.Count > 0)
            {
                TopicAggregator topicAggregator = new TopicAggregator(activeTopics.SelectedItems[0].ToString());
                topicAggregator.Show();
            }
        }
    }
}
