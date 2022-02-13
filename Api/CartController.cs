using AspNetCoreHero.ToastNotification.Abstractions;
using Elderson.Models;
using Elderson.Pages;
using Elderson.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Elderson.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly EldersonContext _context;
        private readonly INotyfService _notfy;
        private BookingService _svc;
        private readonly ILogger<CartModel> _logger;
        private ScheduleService _sSvc;

        public CartController(EldersonContext context, BookingService service, ScheduleService sService, ILogger<CartModel> logger, INotyfService notyf)
        {
            _context = context;
            _svc = service;
            _sSvc = sService;
            _logger = logger;
            _notfy = notyf;
        }

        [HttpGet]
        public ActionResult<List<CartItem>> Get()
        {
            try
            {
                List<CartItem> cartItems = new List<CartItem>();
                cartItems = HttpContext.Session.GetCart("Cart");
                var jsonStr = JsonSerializer.Serialize(cartItems);
                return Ok(jsonStr);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }   
        }
        [HttpGet("{apiname}/{Id}", Name = "RemoveCartItem")]
        public void Get(int Id)
        {
            List<CartItem> cartItems = new List<CartItem>();
            cartItems = HttpContext.Session.GetCart("Cart");
            cartItems.RemoveAt(Id);
            HttpContext.Session.SetCart("Cart", cartItems);
            _notfy.Warning("Item removed!");
        }
        [HttpPost]
        public ActionResult<string> Post([FromForm] List<CartItem> body)
        {
            bool valid = true;
            if (!ModelState.IsValid)
            {
                _notfy.Error("Error");
                return BadRequest("Error");
            }
            if (valid)
            {
                foreach (CartItem item in body)
                {
                    string bookUUID = Guid.NewGuid().ToString();
                    string callUUID = Guid.NewGuid().ToString();
                    Booking book = new Booking();
                    book.Id = bookUUID;
                    book.Clinic = item.Clinic;
                    book.BookDateTime = Convert.ToDateTime(item.BookDateTime);
                    book.Symptoms = item.Symptoms;
                    book.PatientID = HttpContext.Session.GetString("LoginUser");
                    book.DoctorID = item.DoctorID;
                    book.CallUUID = callUUID;
                    _svc.AddBooking(book);
                    Schedule schedule = _sSvc.GetScheduleByDateTime(Convert.ToDateTime(item.BookDateTime));
                    schedule.Availability = "B";
                    _sSvc.UpdateScheduleStatus(schedule);
                    HttpContext.Session.Remove("Cart");
                    _notfy.Success("Items purchased successfully. You can review your bookings in Appointments");
                    return Ok("Success");
                }
            }
            return BadRequest("Error");
        }
    }
}
