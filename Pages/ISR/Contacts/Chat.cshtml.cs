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
        private readonly EldersonContext _context;
        private ChatService _svc;
        public ChatModel(EldersonContext context, ChatService service)
        {
            _context = context;
            _svc = service;
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
            } else
            {
                allmessages = new Dictionary<string, List<Message>>();
            }
            
        }
    }
}
