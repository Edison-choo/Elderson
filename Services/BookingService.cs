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
            AllUserBookings = _context.Bookings.Where(b => b.PatientID == id).ToList();
            return AllUserBookings;
        }
        public bool AddBooking(Booking booking)
        {
            _context.Add(booking);
            _context.SaveChanges();
            return true;
        }
    }
}
