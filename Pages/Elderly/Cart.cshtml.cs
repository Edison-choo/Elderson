using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elderson.Models;
using Elderson.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Elderson.Pages
{
    public class CartModel : PageModel
    {
        [BindProperty]
        public List<CartItem> myCart { get; set; }
        [BindProperty]
        public int myTotal { get; set; } = 0;
        private BookingService _svc;
        private readonly ILogger<CartModel> _logger;
        public CartModel(ILogger<CartModel> logger, BookingService service)
        {
            _svc = service;
            _logger = logger;
        }
        public void OnGet()
        {
            if (HttpContext.Session.GetCart("Cart") != null)
            {
                myCart = HttpContext.Session.GetCart("Cart");
                foreach(CartItem item in myCart)
                {
                    myTotal += item.Price;
                }
            }
        }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                myCart = HttpContext.Session.GetCart("Cart");
                foreach (CartItem item in myCart)
                {
                    string bookUUID = Guid.NewGuid().ToString();
                    string callUUID = Guid.NewGuid().ToString();
                    Booking book = new Booking();
                    book.Id = bookUUID;
                    book.Clinic = item.Clinic;
                    book.BookDateTime = Convert.ToDateTime(item.BookDateTime);
                    book.Symptoms = item.Symptoms;
                    book.PatientID = "1";
                    book.DoctorID = "2";
                    book.CallUUID = callUUID;
                    _svc.AddBooking(book);
                }
            }
            return RedirectToPage("Elderly");
        }
    }
    public static class SessionExtensions
    {
        public static void SetCart(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }
        public static List<CartItem> GetCart(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(List<CartItem>) : JsonConvert.DeserializeObject<List<CartItem>>(value);
        }
    }
}
