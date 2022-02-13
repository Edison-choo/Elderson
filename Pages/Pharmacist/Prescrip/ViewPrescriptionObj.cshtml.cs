using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elderson.Models;
using Elderson.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Elderson.Pages.Pharmacist.Prescrip
{
    public class ViewPrescriptionObjModel : PageModel
    {
        [BindProperty]
        public Prescription SelectedPrescripiton { get; set; }
        [BindProperty]
        public FormMeds SelectedMedication { get; set; }
        public List<FormMeds> medication { get; set; }
        public List<string> specificMedication { get; set; }

        public Prescription updatedPrescription { get; set; }
        private FormMedsService _svc;
        private PrescriptionService _pres_svc;
        public ViewPrescriptionObjModel(FormMedsService service, PrescriptionService prescription_service)
        {
            _svc = service;
            _pres_svc = prescription_service;
        }
        public void OnGet(string id, string formId)
        {
            SelectedPrescripiton = _pres_svc.GetPrescriptionByID(id);
            medication = _svc.GetFormMedsByFormId(formId);
            specificMedication = new List<string>();
            foreach(var m in medication)
            {
                if (SelectedPrescripiton.FormId != null && SelectedPrescripiton.FormId.Equals(m.FormId))
                {
                    specificMedication.Add(String.Format("{0}: {1}", m.MedName, m.MedType));
                }
                else
                {
                    medication = null;
                }
            }
                
        }

        public IActionResult OnPost()
        {
            if (!(ModelState.IsValid))
            {
                return Page();
            }

            updatedPrescription = _pres_svc.GetPrescriptionByID(SelectedPrescripiton.Id);

            if (_pres_svc.GetPrescriptionByID(SelectedPrescripiton.Id) != null && SelectedPrescripiton.Id != SelectedPrescripiton.Id)
            {
                return Page();
            }
            updatedPrescription.Status = "1";
            Boolean Valid = _pres_svc.UpdatePrescription(updatedPrescription);
            if (Valid)
            {
                return RedirectToPage("PastPrescriptions");
            }

            return Page();
        }
    }
}
