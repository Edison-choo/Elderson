using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Elderson.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Elderson.Pages
{
    public class SymptomsModel : PageModel
    {
        [BindProperty]
        public List<CartItem> myCart { get; set; }
        CartItem cartItem = new CartItem();
        [BindProperty]
        [Required]
        public string myClinic { get; set; }
        [BindProperty]
        [Required]
        public string myDoctor { get; set; }
        [BindProperty]
        [Required]
        public string myDate { get; set; }
        [BindProperty]
        [Required]
        public string myTime { get; set; }
        [BindProperty]
        [Required]
        public string myDateTime { get; set; }
        [BindProperty]
        [Required]
        public string mySymptoms { get; set; }
        public void OnGet()
        {
            if (HttpContext.Session.GetCart("Cart") != null)
            {
                myCart = HttpContext.Session.GetCart("Cart");
            }
            if (HttpContext.Session.GetString("myDateTime") != null)
            {
                myClinic = HttpContext.Session.GetString("myClinic");
                myDoctor = HttpContext.Session.GetString("myDoctor");
                myDate = HttpContext.Session.GetString("myDate");
                myTime = HttpContext.Session.GetString("myTime");
                myDateTime = HttpContext.Session.GetString("myDateTime");
            }
            else
            {
                Response.Redirect("DateAndTime");
            }
        }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                cartItem.ItemName = "Online Consultation";
                cartItem.Price = 20;
                cartItem.Quantity = 1;
                cartItem.Clinic = myClinic;
                cartItem.BookDateTime = HttpContext.Session.GetString("myDateTime");
                cartItem.Symptoms = mySymptoms;
                cartItem.DoctorID = "1";
                myCart.Add(cartItem);
                HttpContext.Session.SetCart("Cart", myCart);
            }
            return RedirectToPage("Cart");

        }
    }
}
