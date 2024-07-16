using BookYourRoom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookYourRoom.Services.Rooms
{
    public interface IRoomService
    {
        public Task<IEnumerable<Room>> GetAllRooms();

        public Task<Room?> GetRoomById(int roomId);

        public Task CreateRoom(Room room);

        public Task UpdateRoom(Room room);

        public Task DeleteRoom(int roomId);
    }
}
