using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace NetusClientWpf {
    public class NetusClient {
        private readonly string _ip;
        private readonly int _port;
        private readonly TcpClient _client;


        public NetusClient(string ip, int port) {
            _ip = ip;
            _port = port;
            _client = new TcpClient();
            
        }

        public async Task Connect() => await _client.ConnectAsync(IPAddress.Parse(_ip), _port);

        public async Task<string> ReadMessage() {
            return await new StreamReader(_client.GetStream()).ReadLineAsync();
        }

        public async Task SendMessage(string message) {
            var buffer = Encoding.ASCII.GetBytes(message + Environment.NewLine);
            await _client.GetStream().WriteAsync(buffer, 0, buffer.Length);
        }
    }
}