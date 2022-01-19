using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elderson.Models;
using Elderson.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebSocketSharp;

namespace Elderson.Pages.ISR.Users
{
    public class ChatModel : PageModel
    {
        [BindProperty]
        public Message messages { get; set; }
        [BindProperty]
        public Dictionary<string, List<Message>> allmessages { get; set; }
        [BindProperty]
        public Dictionary<string, User> allusers { get; set; }
        private readonly EldersonContext _context;
        private ChatService _svc;
        private UserService _u_svc;
        public ChatModel(EldersonContext context, ChatService service, UserService userService)
        {
            _context = context;
            _svc = service;
            _u_svc = userService;
        }
        public void OnGet()
        {
            //using (WebSocket ws = new WebSocket("ws://localhost:44311"))
            //{
            //    ws.OnMessage += (sender, e) =>
            //    {
            //        if (e.IsText)
            //        {
            //            // Do something with e.Data.
            //            return;
            //        }

            //        if (e.IsBinary)
            //        {
            //            // Do something with e.RawData.
            //            return;
            //        }
            //        Console.WriteLine("Laputa says: " + e.Data);
            //    };

            //    ws.OnError += (sender, e) => {
            //        return;
            //    };


            //    ws.Connect();
            //    ws.Send("BALUS");
            //    Console.ReadKey(true);
            //}

            //messages = _context.Messages.ToList();
            if (HttpContext.Session.GetString("LoginUser") != null)
            {
                allmessages = _svc.GetAllChats(HttpContext.Session.GetString("LoginUser"));
                allusers = new Dictionary<string, User>();
                foreach (var user in allmessages)
                {
                    allusers[user.Key] = _u_svc.GetUserById(user.Key);
                }
            } else
            {
                allmessages = new Dictionary<string, List<Message>>();
                allusers = new Dictionary<string, User>();
            }
            
        }
    }
}
