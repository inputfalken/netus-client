using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
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
        private static readonly NetusClient NetusClient = new NetusClient("10.0.2.15", 23000);

        public MainWindow() {
            InitializeComponent();
            Loaded += OnLoaded;
            SendBtn.Click += SendBtnOnClick;
        }


        private async void SendBtnOnClick(object sender, RoutedEventArgs routedEventArgs) {
            var messageBoxText = MessageBox.Text;
            await NetusClient.SendMessage(messageBoxText);
            AddToChatBox(messageBoxText);
        }

        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs) {
            await NetusClient.Connect();
            AddToChatBox(await NetusClient.ReadMessage());
        }

        private void AddToChatBox(string message) => ChatBox.Items.Add(message);
    }
}