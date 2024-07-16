using BookYourRoom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookYourRoom.Services.Bookings
{
    public interface IBookingService
    {
        public Task<IEnumerable<Booking>> GetAllBookings();

        public Task<Booking?> GetBookingById(int bookingId);

        public Task CreateBooking(Booking booking);

        public Task UpdateBooking(Booking booking);

        public Task DeleteBooking(int bookingId);
    }
}
