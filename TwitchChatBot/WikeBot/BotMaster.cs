using System;
using System.Net;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using LogWriting;

namespace WikeBot
{
    public static class BotMaster
    {
        private static string _userName = "wikedabot";
        private static string _streamName = "wike";
        private static string _oAuth = "";
        private static LogWriter _messageLog = new LogWriter(@"logs\chat-messages");
        private static LogWriter _eventLog = new LogWriter(@"logs\irc-messages");

        public static void MainLoop()
        {
            GetOAuth();
            var client = new IRCClient("irc.twitch.tv", 6667, _userName, _oAuth, _streamName);
            
            bool exit = false;
            while (!exit)
            {
                string ircMessage = client.ReadMessage();

                _eventLog.NewLogEntry(ircMessage);

                if (ircMessage.Contains("PRIVMSG"))
                {
                    string messengerName = ircMessage[1..ircMessage.IndexOf("!")];
                    string onlyMessage = ircMessage.Substring(ircMessage.IndexOf(":", 1) + 1);
                    string fullUserMessage = $"{messengerName} :{onlyMessage}";
                    
                    _messageLog.NewLogEntry(fullUserMessage);

                    if (onlyMessage == "!exit" && ircMessage.Substring(0, 20).Contains("wike"))
                    {
                        RespondInChat(client, "wike says its time for me to go.., bye all");
                        exit = true;
                    }
                    else if (onlyMessage == "!help")
                    {
                        RespondInChat(client, $"{messengerName}... I cannot help you. No one can help you.");
                    }
                }
            }
        }

        private static void RespondInChat(IRCClient client, string response)
        {
            client.SendChatMessage(response);
            _messageLog.NewLogEntry("wikedabot :" + response);
            _eventLog.NewLogEntry("wikedabot :" + response);
        }

        private static void GetOAuth()
        {
            _oAuth = ""; // Add code here to read OAuth from a file
        }
    }
}
