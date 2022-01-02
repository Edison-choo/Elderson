using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Elderson.Models;
using Elderson.Services;
using System.Globalization;
using Microsoft.Extensions.Logging;

namespace Elderson.Pages.ISR.Users
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public User SelectedUser { get; set; }
        [BindProperty]
        public Administrator AdminRole { get; set; }
        [BindProperty]
        public Doctor DoctorRole { get; set; }
        [BindProperty]
        public Patient PatientRole { get; set; }
        [BindProperty]
        public string Birthdate { get; set; }
        public User UpdatedUser { get; set; }
        public Administrator UpdatedAdmin { get; set; }
        public Doctor UpdatedDoctor { get; set; }
        public Patient UpdatedPatient { get; set; }
        private UserService _svc;
        private readonly ILogger<EditModel> _logger;
        public EditModel(ILogger<EditModel> logger, UserService service)
        {
            _logger = logger;
            _svc = service;
        }
        public void OnGet(string id)
        {
            SelectedUser = _svc.GetUserById(id);
            Birthdate = SelectedUser.Birthdate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            PatientRole = _svc.GetPatientById(id);
            AdminRole = _svc.GetAdministratorById(id);
            DoctorRole = _svc.GetDoctorById(id);
        }

        public IActionResult OnPost()
        {
            if (!(ModelState.IsValid))
            {
                return Page();
            }

            UpdatedUser = _svc.GetUserById(SelectedUser.Id);
            if (_svc.GetUserByEmail(SelectedUser.Email) != null && SelectedUser.Email != UpdatedUser.Email)
            {
                _logger.LogInformation($"Edit user {SelectedUser.Id} unsuccessfully. Email is already being used.");
                return Page();
            }
            UpdatedUser.Fullname = SelectedUser.Fullname;
            UpdatedUser.Email = SelectedUser.Email;
            UpdatedUser.Phone = SelectedUser.Phone;
            UpdatedUser.Gender = SelectedUser.Gender;
            UpdatedUser.CountryCode = SelectedUser.CountryCode;
            UpdatedUser.Birthdate = Convert.ToDateTime(Birthdate);
            Boolean valid = _svc.UpdateUser(UpdatedUser);

            switch (UpdatedUser.UserType)
            {
                case "Patient":
                    UpdatedPatient = _svc.GetPatientById(UpdatedUser.Id);
                    UpdatedPatient.Nric = PatientRole.Nric;
                    UpdatedPatient.Relationship = PatientRole.Relationship;
                    UpdatedPatient.EmergencyName = PatientRole.EmergencyName;
                    UpdatedPatient.EmergencyNum = PatientRole.EmergencyNum;
                    UpdatedPatient.HomeAddr = PatientRole.HomeAddr;
                    UpdatedPatient.CountryCode = PatientRole.CountryCode;
                    _svc.UpdatePatient(UpdatedPatient);
                    break;
                case "Doctor":
                    UpdatedDoctor = _svc.GetDoctorById(UpdatedUser.Id);
                    UpdatedDoctor.Clinic = DoctorRole.Clinic;
                    _svc.UpdateDoctor(UpdatedDoctor);
                    break;
                case "Administrator":
                    UpdatedAdmin = _svc.GetAdministratorById(UpdatedUser.Id);
                    UpdatedAdmin.Clinic = AdminRole.Clinic;
                    UpdatedAdmin.OpeningHours = AdminRole.OpeningHours;                                                                                                                                                                                                                                                                                                                                                                             
                    UpdatedAdmin.ClosingHours = AdminRole.ClosingHours;
                    _svc.UpdateAdministrator(UpdatedAdmin);
                    break;
            }
            if (valid)
            {
                _logger.LogInformation($"Edit user {SelectedUser.Id} successfully.");
                return RedirectToPage("Index");
            }

            return Page();
        }
    }
}
