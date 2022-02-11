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
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;

namespace Elderson.Pages.Organization
{
    public class EditModel : PageModel
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
        public string Birthdate { get; set; }
        [BindProperty]
        public Clinic selectedClinic { get; set; }
        public List<Clinic> allClinic { get; set; }
        [BindProperty]
        public List<SelectListItem> clinics { get; set; }
        public User UpdatedUser { get; set; }
        public Elderson.Models.Administrator UpdatedAdmin { get; set; }
        public Doctor UpdatedDoctor { get; set; }
        public Patient UpdatedPatient { get; set; }
        public Clinic UpdatedClinic { get; set; }
        private UserService _svc;
        private readonly ILogger<EditModel> _logger;
        private readonly INotyfService _notfy;
        public EditModel(ILogger<EditModel> logger, UserService service, INotyfService notyf)
        {
            _logger = logger;
            _svc = service;
            _notfy = notyf;
        }
        public void OnGet()
        {
            var id = HttpContext.Session.GetString("LoginUser");
            id = "2bf4dd3d-616e-4836-9954-6f12e3e3d8d1";
            SelectedUser = _svc.GetUserById(id);
            Birthdate = SelectedUser.Birthdate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            PatientRole = _svc.GetPatientById(id);
            AdminRole = _svc.GetAdministratorById(id);
            DoctorRole = _svc.GetDoctorById(id);
            allClinic = _svc.GetAllClinic();
            clinics = new List<SelectListItem>();
            foreach (var i in allClinic)
            {
                clinics.Add(new SelectListItem { Text = i.Name, Value = i.Id });
            }
        }

        public IActionResult OnPost()
        {
            if (!(ModelState.IsValid))
            {
                _notfy.Error("Error");
                return Page();
            }

            UpdatedUser = _svc.GetUserById(SelectedUser.Id);
            if (_svc.GetUserByEmail(SelectedUser.Email) != null && SelectedUser.Email != UpdatedUser.Email)
            {
                _logger.LogInformation("{actionStatus} User {userId} {userAction}. Email is already being used.", "Unsuccessful", SelectedUser.Id, "edit user");
                _notfy.Error("Email is already used");
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

                    UpdatedDoctor.ClinicId = DoctorRole.ClinicId;
                    UpdatedDoctor.Language = DoctorRole.Language;
                    UpdatedDoctor.WorkExp = DoctorRole.WorkExp;
                    _svc.UpdateDoctor(UpdatedDoctor);
                    break;
                case "Administrator":
                    UpdatedAdmin = _svc.GetAdministratorById(UpdatedUser.Id);
                    UpdatedAdmin.ClinicId = AdminRole.ClinicId;
                    _svc.UpdateAdministrator(UpdatedAdmin);
                    break;
            }
            if (valid)
            {
                _logger.LogInformation("{actionStatus} User {userId} {userAction}.", "Successful", SelectedUser.Id, "edit user");
                _notfy.Success("Edit User Successfully");
                return RedirectToPage("Profile");

            }

            return Page();
        }
    }
}
