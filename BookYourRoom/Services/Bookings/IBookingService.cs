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
        public IEnumerable<Booking> GetAllBookings();

        public Booking? GetBookingById(int bookingId);

        public void CreateBooking(Booking booking);

        public void UpdateBooking(Booking booking);

        public void DeleteBooking(int bookingId);
    }
}
