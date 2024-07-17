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


        public async Task CreateHotel(Hotel hotel)
        {
            bool isHotelConflicts = await IsHotelConflicts(hotel);
            if (!isHotelConflicts)
            {
                _context.Hotels.Add(hotel);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentException("There is already a hotel with the same address.");
            }
        }

        public async Task UpdateHotel(Hotel newHotel)
        {
            try
            {
                var existingHotel = await _context.Hotels.FindAsync(newHotel.HotelId);
                if (existingHotel != null)
                {
                    bool isHotelConflicts = await IsHotelConflicts(newHotel);
                    if (!isHotelConflicts)
                    {
                        _context.Entry(existingHotel).CurrentValues.SetValues(newHotel);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        throw new ArgumentException("There is already a hotel with the same address.");
                    }
                }
                else
                {
                    throw new InvalidOperationException("Hotel not found.");
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to update the hotel.", ex);
            }
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

        public async Task<bool> IsHotelConflicts(Hotel hotel)
        {
            if (_context.Hotels.ToList().Count == 0) return false;
            var hotelsWithSameAddress = await _context.Hotels.Where(h => h.Address == hotel.Address).ToListAsync();
            return hotelsWithSameAddress.Any();
        }
    }
}
