using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace NetusClientWpf {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        private readonly NetusClient _netusClient;

        public MainWindow(NetusClient netusClient) {
            InitializeComponent();
            _netusClient = netusClient;
            Loaded += OnLoaded;
            SendBtn.Click += SendBtnOnClick;
        }


        private async void SendBtnOnClick(object sender, RoutedEventArgs routedEventArgs) {
            var messageBoxText = MessageInputBox.Text;
            //Server needs to send back the message with a timestamp for when message was recieved.
            await _netusClient.SendMessage(messageBoxText);
            AddToChatBox(messageBoxText);
        }

        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs) {
            await ListenForMessages();
        }

        private async Task ListenForMessages() {
            while (true) {
                var s = await Task.Run(() => _netusClient.ReadMessage());
                AddToChatBox(s);
            }
        }

        private void AddToChatBox(string message) => ChatBox.Items.Add(message);
    }
}