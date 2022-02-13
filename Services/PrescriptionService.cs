using Elderson.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elderson.Services
{
    public class PrescriptionService
    {
        private EldersonContext _context;
        public PrescriptionService(EldersonContext context)
        {
            _context = context;
        }

        private bool PrescriptionExist(string id)
        {
            return _context.Prescription.Any(e => e.Id == id);
        }

        public List<Prescription> GetAllPrescriptions()
        {
            List<Prescription> allPrescriptions = new List<Prescription>();
            allPrescriptions = _context.Prescription.ToList();
            return allPrescriptions;
        }

        public List<Prescription> GetPrescriptionsOld()
        {
            List<Prescription> oldprescriptions = new List<Prescription>();
            oldprescriptions = _context.Prescription.Where(e => e.Status == "1").ToList();
            return oldprescriptions;
        }

        public List<Prescription> GetPrescriptionsNew()
        {
            List<Prescription> newPrescriptions = new List<Prescription>();
            newPrescriptions = _context.Prescription.Where(e => e.Status == "0").ToList();
            return newPrescriptions;
        }

        public bool AddPrescription(Prescription prescription)
        {
            if (PrescriptionExist(prescription.Id))
            {
                return false;
            }
            _context.Add(prescription);
            _context.SaveChanges();
            return true;
        }

        
    }
}
