using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elderson.Models;
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
        private readonly EldersonContext _context;
        public ChatModel(EldersonContext context)
        {
            _context = context;
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
        }
    }
}
