using System;
using System.IO;
using LogWriter;

namespace WikeBot
{
    public static class ChatBotMaster
    {
        private static string _userName;
        private static string _streamName;
        private static string _oAuth;
        ///
        private static ILogWriter _messageLog = new LogFileWriter(@"logs\chat-messages");
        private static ILogWriter _eventLog = new LogFileWriter(@"logs\irc-messages");
        private static ILogWriter _errorLog = new LogFileWriter(@"logs\error");
        /* 
        //  Optionally save messages to a database
        //  Connection string and DB table name is required
        string connectionString = "";
        private static ILogWriter _messageLog = new LogDBWriter(connectionString,"message_log");
        private static ILogWriter _eventLog = new LogDBWriter(connectionString,"event_log");
        private static ILogWriter _errorLog = new LogDBWriter(connectionString,"error_log");
        */
        public static IRCClient client;
        private static Ping pinger;

        public static void MainLoop()
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
                    _eventLog.NewLogEntry("PONG");
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

        /// <summary>
        /// Initializes connection to Twitch IRC server
        /// </summary>
        private static void EstablishConnection()
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

        private static void RespondInChat(IRCClient client, string response)
        {
            client.SendChatMessage(response);
            _messageLog.NewLogEntry("wikedabot :" + response);
            _eventLog.NewLogEntry("wikedabot :" + response);
        }

        private static void GetCredentials()
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
