using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Chat.Client.Model;
using Microsoft.AspNetCore.SignalR.Client;
using Qml.Net;

namespace Chat.Client.Controllers
{
    public class ChatController
    {
        private readonly string _user;
        private readonly HubConnection _connection;

        public ChatController()
        {
            _user = Guid.NewGuid().ToString();
            _connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:5000/chatHub")
                .Build();

            _connection.Closed += async (error) =>
            {
                AddChatMessage(ChatMessage.SystemMessage("Disconnected"));
                await Task.Delay(1500);
                await Connect();
            };
        }

        [NotifySignal]
        public IList<ChatMessage> ChatMessages { get; } = new List<ChatMessage>();

        public async Task Connect()
        {
            _connection.On<string, string, DateTime>("ReceiveMessage", ReceiveMessage);

            try
            {
                AddChatMessage(ChatMessage.SystemMessage("Connecting..."));
                await _connection.StartAsync();
                AddChatMessage(ChatMessage.SystemMessage("Connected."));
            }
            catch (Exception ex)
            {
                AddChatMessage(ChatMessage.SystemMessage(ex.Message));
            }
        }

        public void ReceiveMessage(string user, string message, DateTime timestamp)
        {
            AddChatMessage(new ChatMessage(user, message, timestamp));
        }

        public async Task SendMessage(string message)
        {
            try
            {
                await _connection.InvokeAsync("SendMessage", _user, message);
            }
            catch (Exception ex)
            {
                AddChatMessage(ChatMessage.SystemMessage(ex.Message));
            }
        }

        public string ToLocalDateTimeString(DateTime dateTime)
        {
            var convertedDate = DateTime.SpecifyKind(dateTime, DateTimeKind.Utc);
            return convertedDate.ToLongTimeString();
        }

        private void AddChatMessage(ChatMessage chatMessage)
        {
            ChatMessages.Add(chatMessage);
            this.ActivateSignal("chatMessagesChanged");
        }
    }
}