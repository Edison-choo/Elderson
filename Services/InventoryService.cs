using Elderson.Models;
using Microsoft.EntityFrameworkCore;
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

        public Medication GetMedicationById(string id)
        {
            Medication medication = _context.Medications.Where(e => e.Id == id).FirstOrDefault();
            return medication;
        }

        public MedInventory GetInvMedicationById(string id)
        {
            MedInventory inventory = _context.Inventory.Where(e => e.Id == id).FirstOrDefault();
            return inventory;
        }

        public Medication GetMedicationByName(string name)
        {
            Medication medication = _context.Medications.Where(e => e.MedName == name).FirstOrDefault();
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

        public bool UpdateMedication(Medication medication)
        {
            bool updated = true;
            _context.Attach(medication).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
                updated = true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MedicationExists(medication.Id))
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

        public bool UpdateInventory(MedInventory inventory)
        {
            bool updated = true;
            _context.Attach(inventory).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
                updated = true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MedicationExists(inventory.Id))
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

        public bool DeleteInventory(MedInventory theInventory)
        {
            bool deleted = true;
            _context.Attach(theInventory).State = EntityState.Modified;

            try
            {
                _context.Remove(theInventory);
                _context.SaveChanges();
                deleted = true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MedicationExists(theInventory.Id))
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

        public bool DeleteMedication(Medication theMedication)
        {
            bool deleted = true;
            _context.Attach(theMedication).State = EntityState.Modified;

            try
            {
                _context.Remove(theMedication);
                _context.SaveChanges();
                deleted = true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MedicationExists(theMedication.Id))
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
    }
}
