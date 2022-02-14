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
        [HttpGet("{apiname}", Name = "GetMedication")]
        public ActionResult<List<CartItem>> GetMedication()
        {
            try
            {
                List<CartMedication> cartItems = new List<CartMedication>();
                cartItems = HttpContext.Session.GetMedicationCart("MedicationCart");
                var jsonStr = JsonSerializer.Serialize(cartItems);
                return Ok(jsonStr);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpGet("{apiname}/{Id}", Name = "RemoveCartItem")]
        public void RemoveCartItem(int Id)
        {
            List<CartItem> cartItems = new List<CartItem>();
            cartItems = HttpContext.Session.GetCart("Cart");
            cartItems.RemoveAt(Id);
            HttpContext.Session.SetCart("Cart", cartItems);
            _notfy.Warning("Item removed!");
        }
    }
}
