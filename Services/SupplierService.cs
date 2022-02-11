using Elderson.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elderson.Services
{
    public class SupplierService
    {
        private EldersonContext _context;

        public SupplierService(EldersonContext context)
        {
            _context = context;
        }

        public List<Supplier> GetAllSuppliers()
        {
            List<Supplier> AllSuppliers = new List<Supplier>();
            AllSuppliers = _context.Supplier.ToList();
            return AllSuppliers;
        }

        public Supplier GetSupplierbyId(string id)
        {
            Supplier supplier = _context.Supplier.Where(e => e.Id == id).FirstOrDefault();
            return supplier;
        }

        private bool SupplierExists(string id)
        {
            return _context.Supplier.Any(e => e.Id == id);
        }

        public bool AddSupplier(Supplier supplier)
        {
            if (SupplierExists(supplier.Id))
            {
                return false;
            }
            _context.Add(supplier);
            _context.SaveChanges();
            return true;
        }


        public bool UpdateSupplier(Supplier supplier)
        {
            bool updated = true;
            _context.Attach(supplier).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
                updated = true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SupplierExists(supplier.Id))
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

        public bool DeleteSupplier(Supplier theSupplier)
        {
            bool deleted = true;
            _context.Attach(theSupplier).State = EntityState.Modified;

            try
            {
                _context.Remove(theSupplier);
                _context.SaveChanges();
                deleted = true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SupplierExists(theSupplier.Id))
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
