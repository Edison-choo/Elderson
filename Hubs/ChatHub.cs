using Elderson.Models;
using Elderson.Services;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elderson.Hubs
{
    public class ChatHub: Hub
    {
        private ChatService _svc;
        public ChatHub(ChatService service)
        {
            _svc = service;
        }
        public async Task SendMessage(Message message)
        {
            Console.WriteLine(message.UserId + message.Text + message.ToUserId);

            string guid = Guid.NewGuid().ToString();
            message.Id = guid;
            message.When = DateTime.Now;
            _svc.AddChat(message);
            await Clients.All.SendAsync("ReceiveMessage", message);
        }

        public string GetConnectionId(string userId)
        {
            return Context.ConnectionId;
        }

        public async Task Send(Message message, string userId)
        {
            Console.WriteLine(message.Text);
            
            string guid = Guid.NewGuid().ToString();
            message.Id = guid;
            message.When = DateTime.Now;
            _svc.AddChat(message);
            await Clients.All.SendAsync("ReceiveMessage", message);
        }

    }
}
