using BookYourRoom.Data;
using BookYourRoom.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookYourRoom.Services.Bookings
{
    public class BookingService : IBookingService
    {
        private readonly HotelContext _context;

        public BookingService(HotelContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Booking>> GetAllBookings()
        {
            return await _context.Bookings.ToListAsync();
        }

        public async Task<Booking?> GetBookingById(int bookingId)
        {
            return await _context.Bookings.FindAsync(bookingId);
        }

        public async Task CreateBooking(Booking booking)
        {
            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateBooking(Booking booking)
        {
            _context.Update(booking);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBooking(int bookingId)
        {
            var booking = await _context.Bookings.FindAsync(bookingId);
            if (booking != null)
            {
                _context.Bookings.Remove(booking);
                await _context.SaveChangesAsync();
            }
        }
    }
}
