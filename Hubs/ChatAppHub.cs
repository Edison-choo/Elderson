using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elderson.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;

namespace Elderson.Hubs
{   
    public class ChatAppHub : Hub
    {
        private BookingService _svc;
        private UserService _uSvc;
        public ChatAppHub(BookingService service, UserService uService)
        {
            _svc = service;
            _uSvc = uService;
        }
        public override async Task OnConnectedAsync()
        {
            string groupname = Context.GetHttpContext().Session.GetString("CallID");
            await Groups.AddToGroupAsync(Context.ConnectionId, groupname);
            await Clients.Groups(groupname).SendAsync("ReceiveMessage", "system", Context.GetHttpContext().Session.GetString("LoginUser")+"0");
            await base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            string groupname = Context.GetHttpContext().Session.GetString("CallID");
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupname);
            await Clients.Groups(groupname).SendAsync("ReceiveMessage", "system", Context.GetHttpContext().Session.GetString("LoginUser") + "1");
            await base.OnDisconnectedAsync(exception);
        }
        public async Task SendMessageToGroup(string groupname, string user, string message)
        {
            await Clients.Group(groupname).SendAsync("ReceiveMessage", user, message);
        }
    }
}
