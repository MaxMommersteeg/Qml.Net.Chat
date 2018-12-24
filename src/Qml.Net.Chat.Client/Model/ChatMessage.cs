using System;

namespace Chat.Client.Model
{
    public class ChatMessage
    {
        private const string SystemUser = "System";

        public ChatMessage(string user, string message, DateTime timestamp)
        {
            User = user;
            Message = message;
            Timestamp = timestamp;
        }

        public string User { get; private set; }

        public string Message { get; private set; }

        public DateTime Timestamp { get; private set; }

        public static ChatMessage SystemMessage(string message)
        {
            return new ChatMessage(SystemUser, message, DateTime.Now);
        }
    }
}
