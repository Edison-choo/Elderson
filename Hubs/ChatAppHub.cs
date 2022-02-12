using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elderson.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;

namespace Elderson.Hubs
{   
    public static class ConnectedUser
    {
        public static List<string> onlineMembers = new List<string>();
    }
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
            string userID = Context.GetHttpContext().Session.GetString("LoginUser");
            string otherID;
            await Groups.AddToGroupAsync(Context.ConnectionId, groupname);
            if (_uSvc.IsDoctor(userID))
            {
                otherID = _svc.GetBookingByCallId(groupname).PatientID;
            }
            else
            {
                otherID = _svc.GetBookingByCallId(groupname).DoctorID;
            }
            ConnectedUser.onlineMembers.Add(userID);
            await Clients.Groups(groupname).SendAsync("ReceiveMessage", "system", userID+"0");
            if (ConnectedUser.onlineMembers.Contains(otherID))
            {
                await Clients.Groups(groupname).SendAsync("ReceiveMessage", "system", otherID + "0");
            }
            await base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            string groupname = Context.GetHttpContext().Session.GetString("CallID");
            string userID = Context.GetHttpContext().Session.GetString("LoginUser");
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupname);
            await Clients.Groups(groupname).SendAsync("ReceiveMessage", "system", userID + "1");
            ConnectedUser.onlineMembers.Remove(userID);
            await base.OnDisconnectedAsync(exception);
        }
        public async Task SendMessageToGroup(string message)
        {
            string groupname = Context.GetHttpContext().Session.GetString("CallID");
            string userID = Context.GetHttpContext().Session.GetString("LoginUser");
            await Clients.Group(groupname).SendAsync("ReceiveMessage", userID, message);
        }
        public async Task SystemMessageEnd()
        {
            string groupname = Context.GetHttpContext().Session.GetString("CallID");
            await Clients.Groups(groupname).SendAsync("ReceiveMessage", "system", groupname + "end");
        }
    }
}
