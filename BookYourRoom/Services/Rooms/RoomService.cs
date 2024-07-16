using BookYourRoom.Data;
using BookYourRoom.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookYourRoom.Services.Rooms
{
    public class RoomService : IRoomService
    {
        private readonly HotelContext _context;

        public RoomService(HotelContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Room>> GetAllRooms()
        {
            return await _context.Rooms.ToListAsync();
        }

        public async Task<Room?> GetRoomById(int roomId)
        {
            return await _context.Rooms.FindAsync(roomId);
        }

        public async Task CreateRoom(Room room)
        {
            _context.Rooms.Add(room);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRoom(Room room)
        {
            _context.Rooms.Update(room);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRoom(int roomId)
        {
            var room = await _context.Rooms.FindAsync(roomId);
            if (room != null)
            {
                _context.Rooms.Remove(room);
                await _context.SaveChangesAsync();
            }
        }
    }
}
