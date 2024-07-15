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
        public IEnumerable<Hotel> GetAllHotels();

        public Hotel? GetHotelById(int hotelId);

        public void CreateHotel(Hotel hotel);

        public void UpdateHotel(Hotel hotel);

        public void DeleteHotel(int hotelId);
    }
}
