using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace NetusClientWpf {
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Window {
        private static readonly NetusClient NetusClient = new NetusClient("192.168.1.90", 23000);

        public Register() {
            InitializeComponent();
            Loaded += OnLoaded;
            RegisterButton.Click += RegisterButtonOnClick;
        }

        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs) {
            try {
                await NetusClient.Connect();
                Label.Content = await NetusClient.ReadMessage();
            }
            catch (Exception) {
                MessageBox.Show("Could not establish an connection to the Server.");
                Close();
            }
        }

        private async void RegisterButtonOnClick(object sender, RoutedEventArgs routedEventArgs) {
            await NetusClient.SendMessage(UserNameBox.Text);
            MessageBox.Show(await NetusClient.ReadMessage());
            new MainWindow(NetusClient).Show();
            Close();
        }
    }
}