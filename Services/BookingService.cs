using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Elderson.Models;

namespace Elderson.Services
{
    public class BookingService
    {
        private EldersonContext _context;

        public BookingService(EldersonContext context)
        {
            _context = context;
        }
        public List<Booking> GetBookingOfUser(string id)
        {
            List<Booking> AllUserBookings = new List<Booking>();
            AllUserBookings = _context.Booking.Where(b => (b.PatientID == id) && (b.BookDateTime > DateTime.Now.AddMinutes(-30)) && (b.Status != "c")).ToList();
            return AllUserBookings;
        }
        public List<Booking> GetBookingHistoryOfUser(string id)
        {
            List<Booking> AllUserBookings = new List<Booking>();
            AllUserBookings = _context.Booking.Where(b => (b.PatientID == id) && (b.BookDateTime > DateTime.Now) && (b.Status == "c")).ToList();
            return AllUserBookings;
        }
        public List<Booking> GetBookingOfDoctor(string id)
        {
            List<Booking> AllUserBookings = new List<Booking>();
            AllUserBookings = _context.Booking.Where(b => (b.DoctorID == id) && (b.BookDateTime > DateTime.Now.AddMinutes(-30))).ToList();
            return AllUserBookings;
        }
        public Booking GetBookingById(string id)
        {
            Booking book = _context.Booking.Where(b => (b.Id == id) && (b.BookDateTime > DateTime.Now.AddMinutes(-30))).FirstOrDefault();
            return book;
        }
        public Booking GetCanRescheduleBookingById(string id)
        {
            Booking book = _context.Booking.Where(b => (b.Id == id) && (b.BookDateTime > DateTime.Now) && (b.Status.ToLower() != "r")).FirstOrDefault();
            return book;
        }
        public Booking GetBookingByCallId(string id)
        {
            Booking book = _context.Booking.Where(b => b.CallUUID == id).FirstOrDefault();
            return book;
        }
        public bool AddBooking(Booking booking)
        {
            _context.Add(booking);
            _context.SaveChanges();
            return true;
        }
        public bool UpdateBooking(Booking booking)
        {
            bool updated = false;
            _context.Attach(booking).State = EntityState.Modified;
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
        public bool DeleteBooking(Booking booking)
        {
            bool deleted = false;
            _context.Attach(booking).State = EntityState.Modified;
            try
            {
                _context.Remove(booking);
                _context.SaveChanges();
                deleted = true;
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return deleted;
        }
        public string getCallIDbyID(string id)
        {
            return _context.Booking.FirstOrDefault(b => b.Id == id).CallUUID;
        }
        public bool isInCall(string id, string userID)
        {
            return _context.Booking.Any(b => (b.Id == id && b.PatientID == userID) || (b.Id == id && b.DoctorID == userID));
        }
    }
}
