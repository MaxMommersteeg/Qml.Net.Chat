using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace SignalRChat.Hubs
{
    public class ChatHub : Hub
    {
        public Task SendMessage(string user, string message)
        {
            var timestamp = DateTime.UtcNow;
            return Clients.All.SendAsync("ReceiveMessage", user, message, timestamp);
        }
    }
}