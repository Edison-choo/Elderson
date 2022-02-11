using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elderson.Models;
using Elderson.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Elderson.Pages.ISR.Clinics
{
    public class DetailModel : PageModel
    {
        [BindProperty]
        public Clinic SelectedClinic { get; set; }
        [BindProperty]
        public Clinic SelectedUser { get; set; }
        [BindProperty]
        public List<User> relatedDoctors { get; set; }
        [BindProperty]
        public List<User> relatedAdmins { get; set; }
        private UserService _svc;
        public DetailModel(UserService service)
        {
            _svc = service;
        }
        public void OnGet(string id)
        {
            SelectedClinic = _svc.GetClinicById(id);
            relatedDoctors = _svc.GetDoctorUsersByClinic(SelectedClinic.Name);
            relatedAdmins = _svc.GetAdminUsersByClinic(SelectedClinic.Name);
        }
    }
}
