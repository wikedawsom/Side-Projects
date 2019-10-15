using System;
using System.Net;
using System.IO;
using System.Net.Sockets;
using System.Threading;

namespace WikeBot
{
    class IRCClient
    {
        private string _channel;
        private TcpClient _tcpClient;
        private StreamReader _sr;
        private StreamWriter _sw;

        public string UserName { get; }

        public IRCClient(string ip, int port, string userName, string password, string channel)
        {
            
        }
    }
}
