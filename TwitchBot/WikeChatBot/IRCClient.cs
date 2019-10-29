using System.IO;
using System.Net.Sockets;

namespace WikeBot
{
    public class IRCClient
    {
        private TcpClient _tcpClient;
        private StreamReader _sr;
        private StreamWriter _sw;

        public string Channel { get; }
        public string UserName { get; }

        public IRCClient(string ip, int port, string userName, string password, string channel)
        {
            UserName = userName;
            Channel = channel;

            _tcpClient = new TcpClient(ip, port);
            _sr = new StreamReader(_tcpClient.GetStream());
            _sw = new StreamWriter(_tcpClient.GetStream());

            _sw.WriteLine("PASS " + password);
            _sw.WriteLine("NICK " + userName);
            _sw.WriteLine("USER " + userName + " 8 * :" + userName);
            _sw.WriteLine("JOIN #" + channel);
            _sw.Flush();
        }

        public void Part()
        {
            SendIRCMessage($":{UserName}!{UserName}@{UserName}.tmi.twitch.tv PART #{Channel}");
        }

        public void SendChatMessage(string message)
        {
            SendIRCMessage(":" + UserName + "!" + UserName + "@" + UserName + ".tmi.twitch.tv PRIVMSG #" + Channel + " :" + message);
        }

        public void SendIRCMessage(string message)
        {
            _sw.WriteLine(message);
            _sw.Flush();
        }

        public string ReadMessage()
        {
            return _sr.ReadLine();
        }
    }
}
