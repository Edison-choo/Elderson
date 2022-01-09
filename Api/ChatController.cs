using Elderson.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elderson.Api
{
    [Authorize]
    public class ChatController : Controller
    {
        private readonly EldersonContext _context;
        public ChatController(EldersonContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            //if (HttpContext.Session.GetString("LoginUser") == null) 
            //    HttpContext.Session.SetString("LoginUser", "");
            var messages = await _context.Messages.ToListAsync();
            return View(messages);
        }
    }
}
