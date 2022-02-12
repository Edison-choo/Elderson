using Elderson.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elderson.Services
{
    public class MedicalHistoryService
    {
        private EldersonContext _context;
        public MedicalHistoryService(EldersonContext context)
        {
            _context = context;
        }
        public List<MedicalHistory> GetMedicalHistoryOfUser(string id)
        {
            return _context.MedicalHistory.Where(m => m.PatientID == id).ToList();
        }
        public MedicalHistory GetMedicalHistoryById(string id)
        {
            return _context.MedicalHistory.Where(m => m.Id == id).FirstOrDefault();
        }
        public bool AddMedicalHistory(MedicalHistory medicalHistory)
        {
            _context.Add(medicalHistory);
            _context.SaveChanges();
            return true;
        }
        public bool UpdateMedicalHistory(MedicalHistory medicalHistory)
        {
            bool updated = false;
            _context.Attach(medicalHistory).State = EntityState.Modified;
            try
            {
                _context.SaveChanges();
                updated = true;
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return updated;
        }
        public bool DeleteMedicalHistory(MedicalHistory medicalHistory)
        {
            bool updated = false;
            _context.Attach(medicalHistory).State = EntityState.Modified;
            try
            {
                _context.Remove(medicalHistory);
                _context.SaveChanges();
                updated = true;
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return updated;
        }
    }
}
