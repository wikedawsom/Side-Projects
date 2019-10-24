using System;
using System.IO;
using LogWriting;

namespace WikeBot
{
    public class ChatBot
    {
        private static string _userName;
        private static string _streamName;
        private static string _oAuth;
        private LogWriter _messageLog = new LogWriter(@"logs\chat-messages");
        private LogWriter _eventLog = new LogWriter(@"logs\irc-messages");
        private LogWriter _errorLog = new LogWriter(@"logs\error");
        public IRCClient client;
        private Ping pinger;

        public void Run()
        {
            EstablishConnection();

            bool exit = false;
            while (!exit)
            {
                string ircMessage = client.ReadMessage();

                _eventLog.NewLogEntry(ircMessage);
                if (ircMessage == "PING :tmi.twitch.tv")
                {
                    client.SendIRCMessage("PONG");
                }
                if (ircMessage.Contains("PRIVMSG"))
                {
                    string messengerName = ircMessage[1..ircMessage.IndexOf("!")];
                    string onlyMessage = ircMessage.Substring(ircMessage.IndexOf(":", 1) + 1);
                    string fullUserMessage = $"{messengerName} :{onlyMessage}";
                    
                    _messageLog.NewLogEntry(fullUserMessage);
                    Console.WriteLine(fullUserMessage);

                    if (onlyMessage == "!exit" && ircMessage.Substring(0, 20).Contains("wike"))
                    {
                        RespondInChat(client, "wike says its time for me to go.., bye all");
                        exit = true;
                    }
                    else if (onlyMessage == "!where are you")
                    {
                        RespondInChat(client, $"{messengerName}, I am here.");
                    }
                }
            }
            client.Part();
        }

        private void EstablishConnection()
        {
            try
            {
                GetCredentials();
                client = new IRCClient("irc.twitch.tv", 6667, _userName, _oAuth, _streamName);
                pinger = new Ping(client);
                pinger.Start();
            }
            catch (Exception ex)
            {
                _errorLog.NewLogEntry(ex.Message + "\n" + ex.StackTrace);
            }
        }

        private void RespondInChat(IRCClient client, string response)
        {
            client.SendChatMessage(response);
            _messageLog.NewLogEntry("wikedabot :" + response);
            _eventLog.NewLogEntry("wikedabot :" + response);
        }

        private void GetCredentials()
        {
            string pathToCreds = "";
            if (File.Exists("PathToInfo.txt"))
            {
                // Get path to credential file
                using (StreamReader sr = new StreamReader("PathToInfo.txt"))
                {
                    pathToCreds = sr.ReadLine();
                }

                // Parse credential file to find oAuth token, username, and IRC channel
                try
                {
                    using (StreamReader sr = new StreamReader(pathToCreds))
                    {
                        while (_oAuth == null || _userName == null || _streamName == null)
                        {
                            string line = sr.ReadLine();
                            if (line != null && line.Length > 0)
                            {
                                if (line.Contains("oauth: "))
                                    _oAuth = line.Substring(line.IndexOf(" ") + 1);

                                else if (line.Contains("username: "))
                                    _userName = line.Substring(line.IndexOf(" ") + 1);

                                else if (line.Contains("chat: "))
                                    _streamName = line.Substring(line.IndexOf(" ") + 1);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    _errorLog.NewLogEntry(ex.Message + "\n" + ex.StackTrace);
                    throw ex;
                }
            }
            else
            {
                // If file does not exist, it is created
                File.Create("PathToInfo.txt");
                // Need to tell user to enter file data
            }
        }
    }
}
