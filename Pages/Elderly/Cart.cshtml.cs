using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elderson.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace Elderson.Pages
{
    public class CartModel : PageModel
    {
        [BindProperty]
        public List<CartItem> myCart { get; set; }
        [BindProperty]
        public int myTotal { get; set; } = 0;
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
            return RedirectToPage("/");
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
