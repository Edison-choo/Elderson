using Elderson.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elderson.Services
{
    public class InventoryService
    {
        private EldersonContext _context;

        public InventoryService(EldersonContext context)
        {
            _context = context;
        }

        public List<MedInventory> GetAllInventories()
        {
            List<MedInventory> AllMedInventories = new List<MedInventory>();
            AllMedInventories = _context.Inventory.ToList();
            return AllMedInventories;
        }

        public List<Medication> GetAllMedications()
        {
            List<Medication> AllMedications = new List<Medication>();
            AllMedications = _context.Medications.ToList();
            return AllMedications;
        }

        

        public MedInventory GetMedicationById(string id)
        {
            MedInventory inventory = _context.Inventory.Where(e => e.Id == id).FirstOrDefault();
            return inventory;
        }

        public Medication GetMedicationByName(string id)
        {
            Medication medication = _context.Medications.Where(e => e.MedName == id).FirstOrDefault();
            return medication;
        }

        private bool MedicationExists(string id)
        {
            return _context.Inventory.Any(e => e.Id == id);
        }

        public bool AddMedicationToInventory(MedInventory inventory)
        {
            if (MedicationExists(inventory.Id))
            {
                return false;
            }
            _context.Add(inventory);
            _context.SaveChanges();
            return true;
        }
        public bool AddMedication(Medication medication)
        {
            if (MedicationExists(medication.Id))
            {
                return false;
            }
            _context.Add(medication);
            _context.SaveChanges();
            return true;
        }
    }
}
