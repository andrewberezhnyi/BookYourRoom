using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookYourRoom.Models
{
    public class Booking
    {
        public int BookingId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int RoomId { get; set; }
        public Room Room { get; set; }
        public string RoomNumber => Room.RoomNumber;
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public string CustomerFullName => Customer.FullName;
    }
}
