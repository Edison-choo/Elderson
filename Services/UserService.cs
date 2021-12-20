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
    
    }
}
