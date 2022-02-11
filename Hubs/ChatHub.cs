using Elderson.Models;
using Elderson.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        private readonly static ConnectionMapping<string> _connections =
            new ConnectionMapping<string>();

        public ChatHub(ChatService service)
        {
            _svc = service;
        }
        public async Task SendMessage(Message message)
        {
            Console.WriteLine(message.UserId + message.Text + message.ToUserId);
            Console.WriteLine(DateTime.Now);

            string guid = Guid.NewGuid().ToString();
            message.Id = guid;
            message.When = DateTime.Now;
            _svc.AddChat(message);
            await Clients.All.SendAsync("ReceiveMessage", message);
        }

        public string GetConnectionId()
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

        public void SendChatMessage(string who, Message message)
        {
            string name = Context.User.Identity.Name;

            foreach (var connectionId in _connections.GetConnections(who))
            {
                Clients.Client(connectionId).SendAsync("ReceiveMessage", message);
            }
        }
        
         public override Task OnConnectedAsync()
        {
            //string userId = Context.GetHttpContext().Request.Query["userId"];
            string userId = Context.GetHttpContext().Session.GetString("LoginUser");
            if (Context.GetHttpContext().Session.GetString("LoginUserType") == "ITSupport")
            {
                Groups.AddToGroupAsync(Context.ConnectionId, "ISRChat");
            }
            Console.WriteLine(userId);
            Groups.AddToGroupAsync(Context.ConnectionId, userId);
            return base.OnConnectedAsync();
        }

        public Task SendMessageToGroup(string receiver, Message message)
        {
            Console.WriteLine(message.UserId + message.Text + message.ToUserId);
            Console.WriteLine(DateTime.Now);

            string guid = Guid.NewGuid().ToString();
            message.Id = guid;
            message.When = DateTime.Now;
            _svc.AddChat(message);
            return Clients.Group(receiver).SendAsync("ReceiveMessage", message);
        }

        public Task SendNotiToGroup(string userId)
        {
            Console.WriteLine(userId);

            return Clients.Group("ISRChat").SendAsync("ReceiveNoti", userId);
        }

        public Task SendNotiToUser(string userId, string itsId)
        {
            Console.WriteLine(userId, itsId);

            return Clients.Group(userId).SendAsync("ReceiveNoti", itsId);
        }

        public Task SendDeleteNoti(string userId, string itsId)
        {
            Console.WriteLine(userId, itsId);

            return Clients.Group(userId).SendAsync("ReceiveDeleteNoti", itsId);
        }
    }
}
