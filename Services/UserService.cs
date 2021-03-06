using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elderson.Models;
using Microsoft.EntityFrameworkCore;

namespace Elderson.Services
{
    public class UserService
    {
        private EldersonContext _context;

        public UserService(EldersonContext context)
        {
            _context = context;
        }
        public List<User> GetAllUsers()
        {
            List<User> AllUsers = new List<User>();
            AllUsers = _context.Users.ToList();
            return AllUsers;
        }
        public User GetUserById(String id)
        {
            User user = _context.Users.Where(e => e.Id == id).FirstOrDefault();
            return user;
        }

        public User GetUserByEmail(String email)
        {
            User user = _context.Users.Where(e => e.Email == email).SingleOrDefault();
            return user;
        }

        public User GetPatientUserByEmail(String email)
        {
            User user = _context.Users.Where(e => e.Email == email && e.UserType == "Patient").FirstOrDefault();
            return user;
        }

        public User GetOtherUserByEmail(String email)
        {
            User user = _context.Users.Where(e => e.Email == email && e.UserType != "Patient").FirstOrDefault();
            return user;
        }

        private bool UserExists(string id)
        {
            return _context.Users.Any(e => e.Id == id);
        }

        public bool AddUser(User user)
        {
            if (UserExists(user.Id))
            {
                return false;
            }
            _context.Add(user);
            _context.SaveChanges();
            return true;
        }

        public bool UpdateUser(User theuser)
        {
            bool updated = true;
            _context.Attach(theuser).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
                updated = true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(theuser.Id))
                {
                    updated = false;
                }
                else
                {
                    throw;
                }
            }
            return updated;
        }

        public bool DeleteUser(User theuser)
        {
            bool deleted = true;
            _context.Attach(theuser).State = EntityState.Modified;

            try
            {
                _context.Remove(theuser);
                _context.SaveChanges();
                deleted = true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(theuser.Id))
                {
                    deleted = false;
                }
                else
                {
                    throw;
                }
            }
            return deleted;
        }

        // Patient Service
        private bool PatientExists(string id)
        {
            return _context.Patients.Any(e => e.Id == id);
        }

        public bool AddPatient(Patient patient)
        {
            if (PatientExists(patient.Id))
            {
                return false;
            }
            _context.Add(patient);
            _context.SaveChanges();
            return true;
        }

        public Patient GetPatientById(String id)
        {
            Patient patient = _context.Patients.Where(e => e.UserId == id).FirstOrDefault();
            return patient;
        }

        public bool DeletePatient(Patient thepatient)
        {
            bool deleted = true;
            _context.Attach(thepatient).State = EntityState.Modified;

            try
            {
                _context.Remove(thepatient);
                _context.SaveChanges();
                deleted = true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PatientExists(thepatient.Id))
                {
                    deleted = false;
                }
                else
                {
                    throw;
                }
            }
            return deleted;
        }

        public bool UpdatePatient(Patient thepatient)
        {
            bool updated = true;
            _context.Attach(thepatient).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
                updated = true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PatientExists(thepatient.Id))
                {
                    updated = false;
                }
                else
                {
                    throw;
                }
            }
            return updated;
        }

        // Doctor Service
        private bool DoctorExists(string id)
        {
            return _context.Doctors.Any(e => e.Id == id);
        }

        public bool AddDoctor(Doctor doctor)
        {
            if (DoctorExists(doctor.Id))
            {
                return false;
            }
            _context.Add(doctor);
            _context.SaveChanges();
            return true;
        }

        public Doctor GetDoctorById(String id)
        {
            Doctor doctor = _context.Doctors.Where(e => e.UserId == id).FirstOrDefault();
            return doctor;
        }

        public bool DeleteDoctor(Doctor thedoctor)
        {
            bool deleted = true;
            _context.Attach(thedoctor).State = EntityState.Modified;

            try
            {
                _context.Remove(thedoctor);
                _context.SaveChanges();
                deleted = true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DoctorExists(thedoctor.Id))
                {
                    deleted = false;
                }
                else
                {
                    throw;
                }
            }
            return deleted;
        }

        public bool UpdateDoctor(Doctor thedoctor)
        {
            bool updated = true;
            _context.Attach(thedoctor).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
                updated = true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DoctorExists(thedoctor.Id))
                {
                    updated = false;
                }
                else
                {
                    throw;
                }
            }
            return updated;
        }

        // Administrator Service
        private bool AdministratorExists(string id)
        {
            return _context.Administrators.Any(e => e.Id == id);
        }

        public bool AddAdministrator(Administrator administrator)
        {
            if (AdministratorExists(administrator.Id))
            {
                return false;
            }
            _context.Add(administrator);
            _context.SaveChanges();
            return true;
        }

        public Administrator GetAdministratorById(String id)
        {
            Administrator administrator = _context.Administrators.Where(e => e.UserId == id).FirstOrDefault();
            return administrator;
        }

        public bool DeleteAdministrator(Administrator theadministrator)
        {
            bool deleted = true;
            _context.Attach(theadministrator).State = EntityState.Modified;

            try
            {
                _context.Remove(theadministrator);
                _context.SaveChanges();
                deleted = true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdministratorExists(theadministrator.Id))
                {
                    deleted = false;
                }
                else
                {
                    throw;
                }
            }
            return deleted;
        }

        public bool UpdateAdministrator(Administrator theadmin)
        {
            bool updated = true;
            _context.Attach(theadmin).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
                updated = true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdministratorExists(theadmin.Id))
                {
                    updated = false;
                }
                else
                {
                    throw;
                }
            }
            return updated;
        }

        //Wye Keong Services :)
        public List<Doctor> GetDoctorsByClinic(string clinic)
        {
            List<Doctor> doctors = new List<Doctor>();
            try
            {
                doctors = _context.Doctors.Where(d => d.Clinic == clinic).ToList();
            }
            catch
            {
                throw;
            }
            return doctors;
        }

    }
}
