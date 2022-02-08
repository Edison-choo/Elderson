using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Elderson.Hubs
{   
    public class ChatAppHub : Hub
    {
        public Task JoinGroup(string groupname)
        {
            return Groups.AddToGroupAsync(Context.ConnectionId, groupname);
        }
        public async Task SendMessageToGroup(string groupname, string user, string message)
        {
            await Clients.Group(groupname).SendAsync("ReceiveMessage", user, message);
        }
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
