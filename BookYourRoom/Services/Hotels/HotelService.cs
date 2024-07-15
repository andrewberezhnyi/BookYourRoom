using BookYourRoom.Data;
using BookYourRoom.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookYourRoom.Services.Hotels
{
    public class HotelService : IHotelService
    {
        private readonly HotelContext _context;

        public HotelService(HotelContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Hotel>> GetAllHotels()
        {
            return await _context.Hotels.ToListAsync();
        }

        public async Task<Hotel?> GetHotelById(int hotelId)
        {
            return await _context.Hotels.FindAsync(hotelId);
        }

        public async Task CreateHotel(Hotel hotel)
        {
            _context.Hotels.Add(hotel);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateHotel(Hotel hotel)
        {
            _context.Hotels.Update(hotel);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteHotel(int hotelId)
        {
            var hotel = await _context.Hotels.FindAsync(hotelId);
            if (hotel != null)
            {
                _context.Hotels.Remove(hotel);
                await _context.SaveChangesAsync();
            }
        }
    }
}
