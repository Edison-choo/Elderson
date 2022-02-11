using Elderson.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elderson.Services;
using AspNetCoreHero.ToastNotification.Abstractions;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace Elderson.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : Controller
    {
        private readonly EldersonContext _context;
        private ChatService _svc;
        private readonly INotyfService _notfy;
        private readonly IWebHostEnvironment webHostEnvironment;
        public ChatController(EldersonContext context, ChatService service, INotyfService notyf, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _svc = service;
            _notfy = notyf;
            webHostEnvironment = hostEnvironment;
        }
        public async Task<IActionResult> Index()
        {
            //if (HttpContext.Session.GetString("LoginUser") == null) 
            //    HttpContext.Session.SetString("LoginUser", "");
            var messages = await _context.Messages.ToListAsync();
            return View(messages);
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{userId}/{toUserId}")]
        public ActionResult Delete(string userId, string toUserId)
        {
            try
            {
                var allChats = _svc.GetAllChats(userId);

                foreach (var chat in allChats[toUserId])
                {
                    _svc.DeleteChat(chat);
                }
                
                _notfy.Success("Delete Chat Successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine("ChatController.DeleteChat", ex);
                return BadRequest();
            }
            return Ok();
        }

        [HttpPost]
        public ActionResult Post()
        {
            if (Request.Form.Files.Count > 0)
            {
                try
                {
                    var files = HttpContext.Request.Form.Files;

                    var uploads = Path.Combine(webHostEnvironment.WebRootPath, "uploads\\images");
                    var extensions = Path.GetExtension(files[0].FileName);

                    var fileName = Guid.NewGuid().ToString().Replace("-", "") + extensions;

                    using (var filestream = new FileStream(Path.Combine(uploads, fileName), FileMode.Create))
                    {
                        files[0].CopyTo(filestream);
                    }
                    Console.WriteLine(DateTime.Now.ToString().Replace(" ", "") + extensions);
                    return Json(fileName);
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }
    }
}
