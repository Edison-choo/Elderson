using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elderson.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Elderson.Pages.Shared
{
    public class DateAndTimeModel : PageModel
    {
        [BindProperty]
        public List<CartItem> cart { get; set; }
        CartItem consultation = new CartItem();
        [BindProperty]
        public string myClinic { get; set; }
        [BindProperty]
        public string myDoctor { get; set; }
        [BindProperty]
        public DateTime myDateTime { get; set; }
        public void OnGet(string clinic, string doctor)
        {
            myClinic = clinic;
            myDoctor = doctor;
            consultation.ItemName = "Online Consultation";
            consultation.Price = 20;
            consultation.Quantity = 1;
            consultation.Clinic = myClinic;
            consultation.BookDateTime = myDateTime;
            consultation.Symptoms = null;
            consultation.PatientID = "1";
            consultation.DoctorID = "1";
        }
        public IActionResult OnPost()
        {
            cart.Add(consultation);
            HttpContext.Session.SetObjectAsJson("Cart", cart);
            return RedirectToPage("Symptoms");
        }
    }
    public static class SessionExtensions
    {
        public static void SetObjectAsJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }
        public static CartItem GetObjectFromJson(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(CartItem) : JsonConvert.DeserializeObject<CartItem>(value);
        }
    }
}
