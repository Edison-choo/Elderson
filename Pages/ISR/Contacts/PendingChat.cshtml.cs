using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elderson.Models;
using Elderson.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Elderson.Pages.ISR.Contacts
{
    public class PendingChatModel : PageModel
    {
        [BindProperty]
        public Message messages { get; set; }
        [BindProperty]
        public Dictionary<string, List<Message>> allmessages { get; set; }
        [BindProperty]
        public Dictionary<string, User> allusers { get; set; }
        private ChatService _svc;
        private UserService _u_svc;

        public PendingChatModel(ChatService service, UserService userService)
        {
            _svc = service;
            _u_svc = userService;
        }
        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("LoginUser") != null)
            {
                if (HttpContext.Session.GetString("LoginUserType") == "ITSupport")
                {

                    allmessages = _svc.GetAllChats("ISRChat");
                    allusers = new Dictionary<string, User>();
                    foreach (var user in allmessages)
                    {
                        allusers[user.Key] = _u_svc.GetUserById(user.Key);
                    }
                    return Page();
                }
            }

            return Redirect("~/");
            

        }
    }
}
