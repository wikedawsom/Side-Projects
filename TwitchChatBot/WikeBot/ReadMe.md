# Twitch Chat Moderator Bot
- Reads chat messages from the Twitch IRC server and from users in a specified channel
- Writes chat and IRC logs to 2 files (one for user messages and one for server messages)
- Processes pre-programmed commands and responds accordingly

### TO DO: 
- read in username, chat channel, and OAuth token from separate file
- create a ping class to keep tcp client connected to server
- add auto-reconnect functionality
- add custom commands (user defined)
- add vip/moderator/broadcaster recognition