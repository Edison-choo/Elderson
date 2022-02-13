using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Elderson.Models;
using Elderson.Services;
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
        public string myClinic { get; set; }
        [BindProperty]
        public string myDoctor { get; set; }
        [BindProperty]
        public string myDoctorName { get; set; }
        [BindProperty]
        public string myDate { get; set; }
        [BindProperty]
        public string myTime { get; set; }
        [BindProperty]
        public string myDateTime { get; set; }
        [BindProperty]
        [Required]
        public string mySymptoms { get; set; }
        private UserService _svc;
        public SymptomsModel(UserService service)
        {
            _svc = service;
        }
        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("LoginUser") != null)
            {
                if (HttpContext.Session.GetString("myDateTime") != null)
                {
                    myClinic = HttpContext.Session.GetString("myClinic");
                    myDoctor = HttpContext.Session.GetString("myDoctor");
                    myDoctorName = _svc.GetUserById(myDoctor).Fullname;
                    myDate = HttpContext.Session.GetString("myDate");
                    myTime = HttpContext.Session.GetString("myTime");
                    myDateTime = HttpContext.Session.GetString("myDateTime");
                    return Page();
                }
                return Redirect("DateAndTime");
            }
            return Redirect("~/");
        }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                if (HttpContext.Session.GetCart("Cart") != null)
                {
                    myCart = HttpContext.Session.GetCart("Cart");
                }
                cartItem.Id = Guid.NewGuid().ToString();
                cartItem.ItemName = "Online Consultation";
                cartItem.Price = 20;
                cartItem.Quantity = 1;
                cartItem.Clinic = HttpContext.Session.GetString("myClinic");
                cartItem.BookDateTime = HttpContext.Session.GetString("myDateTime");
                cartItem.Symptoms = mySymptoms;
                cartItem.DoctorID = HttpContext.Session.GetString("myDoctor");
                myCart.Add(cartItem);
                HttpContext.Session.SetCart("Cart", myCart);
                return RedirectToPage("Cart");
            }
            return RedirectToPage("/");
        }
    }
}
