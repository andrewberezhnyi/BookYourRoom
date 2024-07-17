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
            return await _context.Rooms.Include(h => h.Hotel).ToListAsync();
        }

        public async Task<Room?> GetRoomById(int roomId)
        {
            return await _context.Rooms.FindAsync(roomId);
        }

        public async Task CreateRoom(Room room)
        {
            var roomWithGivenNumber = _context.Rooms.Where(r => r.RoomNumber == room.RoomNumber).FirstOrDefault();
            if (roomWithGivenNumber == null)
            {
                _context.Rooms.Add(room);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentException($"There is already a room No.{room.RoomNumber}");
            }
        }

        public async Task UpdateRoom(Room newRoom)
        {
            try
            {
                var existingRoom = await _context.Rooms.FindAsync(newRoom.RoomId);
                if (existingRoom != null)
                {
                    var roomWithGivenNumber = _context.Rooms.Where(r => r.RoomNumber == newRoom.RoomNumber).FirstOrDefault();
                    if (roomWithGivenNumber == null)
                    {
                        _context.Entry(existingRoom).CurrentValues.SetValues(newRoom);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        throw new ArgumentException($"There is already a room No.{newRoom.RoomNumber}");
                    }
                }
                else
                {
                    throw new InvalidOperationException("Room not found.");
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to update the room.", ex);
            }
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
