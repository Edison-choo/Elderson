using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Elderson.Models;
using Elderson.Services;
using System.Globalization;
using Microsoft.AspNetCore.Http;

namespace Elderson.Pages.Organization
{
    public class ProfileModel : PageModel
    {
        [BindProperty]
        public User SelectedUser { get; set; }
        [BindProperty]
        public Elderson.Models.Administrator AdminRole { get; set; }
        [BindProperty]
        public Doctor DoctorRole { get; set; }
        [BindProperty]
        public Patient PatientRole { get; set; }
        [BindProperty]
        public Clinic SelectedClinic { get; set; }
        [BindProperty]
        public string Birthdate { get; set; }
        private UserService _svc;
        public ProfileModel(UserService service)
        {
            _svc = service;
        }
        public void OnGet()
        {
            var id = HttpContext.Session.GetString("LoginUser");
            id = "8bbe4522-ff24-49f9-bb94-6eff25e16f84";
            SelectedUser = _svc.GetUserById(id);
            PatientRole = _svc.GetPatientById(id);
            AdminRole = _svc.GetAdministratorById(id);
            DoctorRole = _svc.GetDoctorById(id);
            if (DoctorRole != null && DoctorRole.ClinicId != null)
            {
                SelectedClinic = _svc.GetClinicByDoctorId(DoctorRole.ClinicId);
            }
            if ((AdminRole != null && AdminRole.ClinicId != null))
            {
                SelectedClinic = _svc.GetClinicByAdminId(AdminRole.ClinicId);
            }
            Birthdate = SelectedUser.Birthdate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
        }
    }
}