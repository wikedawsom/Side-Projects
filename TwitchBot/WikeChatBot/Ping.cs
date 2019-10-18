using System.Threading;

namespace WikeBot
{
    public class Ping
    {
        public IRCClient _client;
        public Thread _thread1;

        public Ping(IRCClient ircClient)
        {
            _client = ircClient;
            _thread1 = new Thread(new ThreadStart(this.FiveMinutePing));
        }

        public void Start()
        {
            _thread1.IsBackground = true;
            _thread1.Start();
        }

        private void FiveMinutePing()
        {
            while (true)
            {
                _client.SendIRCMessage("PING");
                Thread.Sleep(300000);
            }
        }
    }
}
