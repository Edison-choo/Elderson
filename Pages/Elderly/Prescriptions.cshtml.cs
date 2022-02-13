using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elderson.Models;
using Elderson.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Elderson.Pages.Elderly
{
    public class PrescriptionsModel : PageModel
    {
        [BindProperty]
        public Prescription myPrescription { get; set; }
        [BindProperty]
        public List<Medication> medications { get; set; }
        public List<FormMeds> tempMedList { get; set; }
        [BindProperty]
        public List<CartMedication> cartMedicationList { get; set; }
        [BindProperty]
        public Double total { get; set; } = 0;
        private PrescriptionService _svc;
        private FormMedsService _fsvc;
        private InventoryService _isvc;
        public PrescriptionsModel(PrescriptionService service, FormMedsService fservice, InventoryService iservice)
        {
            _svc = service;
            _fsvc = fservice;
            _isvc = iservice;
        }
        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("LoginUser") != null)
            {
                myPrescription = _svc.GetPrescriptionByPatientID(HttpContext.Session.GetString("LoginUser"));
                tempMedList = _fsvc.GetFormMedsByFormId(myPrescription.FormId);
                foreach (var i in tempMedList)
                {
                    Medication medication = _isvc.GetMedicationById(i.MedicationId);
                    medications.Add(medication);
                    CartMedication cartMedication = new CartMedication();
                    cartMedication.Id = Guid.NewGuid().ToString();
                    cartMedication.DoctorID = myPrescription.DoctorId;
                    cartMedication.ItemName = medication.MedName;
                    cartMedication.Price = _isvc.GetInvMedicationById(medication.Id).Price;
                    cartMedication.Quantity = i.Quantity;
                    cartMedicationList.Add(cartMedication);
                    total += (cartMedication.Price * cartMedication.Quantity);
                }
                return Page();
            }
            return Redirect("~/");
        }
        public IActionResult OnPost()
        {
            HttpContext.Session.SetCart("MedicationCart", cartMedicationList);
            return Redirect("~/Elderly/Cart");
        }
    }
}
