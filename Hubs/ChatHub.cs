using Elderson.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elderson.Hubs
{
    public class ChatHub: Hub
    {
        public async Task SendMessage(Message message)
        {
            Console.WriteLine(message.Text);
            await Clients.All.SendAsync("ReceiveMessage", message);
        }

        public string GetConnectionId()
        {
            return Context.ConnectionId;
        }

        public async Task Send(Message message, string userId)
        {
            Console.WriteLine(message.Text);
            await Clients.Client(userId).SendAsync("ReceiveMessage", message);
        }

    }
}
