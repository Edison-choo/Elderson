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

        public List<MedInventory> GetAllMedInventories()
        {
            List<MedInventory> AllMedInventories = new List<MedInventory>();
            AllMedInventories = _context.Inventory.ToList();
            return AllMedInventories;
        }

        public MedInventory GetMediationById(string id)
        {
            MedInventory inventory = _context.Inventory.Where(e => e.Id == id).FirstOrDefault();
            return inventory;
        }

        private bool MedicationExists(string id)
        {
            return _context.Inventory.Any(e => e.Id == id);
        }

        public bool AddMedication(MedInventory inventory)
        {
            if (MedicationExists(inventory.Id))
            {
                return false;
            }
            _context.Add(inventory);
            _context.SaveChanges();
            return true;
        }
    }
}
