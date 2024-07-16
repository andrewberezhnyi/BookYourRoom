using BookYourRoom.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookYourRoom.Data
{
    public class HotelContext : DbContext
    {
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Customer> Customers { get; set; }

        public HotelContext(DbContextOptions<HotelContext> options) : base(options)
        {
        }
    }
}
