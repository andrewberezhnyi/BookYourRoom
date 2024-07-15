using BookYourRoom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookYourRoom.Services.Hotels
{
    public interface IHotelService
    {
        public Task<IEnumerable<Hotel>> GetAllHotels();

        public Task<Hotel?> GetHotelById(int hotelId);

        public Task CreateHotel(Hotel hotel);

        public Task UpdateHotel(Hotel hotel);

        public Task DeleteHotel(int hotelId);
    }
}
