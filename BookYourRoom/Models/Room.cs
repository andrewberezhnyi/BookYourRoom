using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookYourRoom.Models
{
    public class Room
    {
        public int RoomId { get; set; }
        public string RoomNumber { get; set; }
        public int HotelId { get; set; }
        public Hotel Hotel { get; set; }
        
        public string HotelName => Hotel.Name;
        public ICollection<Booking> Bookings { get; set; }
    }
}
