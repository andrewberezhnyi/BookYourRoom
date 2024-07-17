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
            return await _context.Bookings.Include(c => c.Customer).Include(r => r.Room).ToListAsync();
        }

        public async Task CreateBooking(Booking booking)
        {
            bool isValid = await IsBookingValid(booking);
            if (isValid)
            {
                _context.Bookings.Add(booking);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentException("New reservation conflicting with the old ones");
            }
        }

        public async Task UpdateBooking(Booking newBooking)
        {
            try
            {
                var existingBooking = await _context.Bookings.FindAsync(newBooking.BookingId);
                if (existingBooking != null)
                {
                    bool isValid = await IsBookingValid(newBooking);
                    if (isValid)
                    {
                        _context.Entry(existingBooking).CurrentValues.SetValues(newBooking);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        throw new ArgumentException("New reservation conflicting with the old ones");
                    }
                }
                else
                {
                    throw new InvalidOperationException("Booking not found.");
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to update the booking.", ex);
            }
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

        private async Task<bool> IsBookingValid(Booking newBooking)
        {
            if (_context.Bookings.ToList().Count == 0) return true;
            var conflictingBookings = await _context.Bookings
                .Where(b => b.RoomId == newBooking.RoomId &&
                            ((newBooking.CheckInDate >= b.CheckInDate && newBooking.CheckInDate < b.CheckOutDate) ||
                             (newBooking.CheckOutDate > b.CheckInDate && newBooking.CheckOutDate <= b.CheckOutDate) ||
                             (newBooking.CheckInDate <= b.CheckInDate && newBooking.CheckOutDate >= b.CheckOutDate)))
                .ToListAsync();

            return !conflictingBookings.Any();
        }
    }
}
