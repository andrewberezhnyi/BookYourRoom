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
        public IEnumerable<Room> GetAllRooms();

        public Room? GetRoomById(int roomId);

        public void CreateRoom(Room room);

        public void UpdateRoom(Room room);

        public void DeleteRoom(int roomId);
    }
}
