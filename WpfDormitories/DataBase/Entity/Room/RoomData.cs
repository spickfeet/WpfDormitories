using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfDormitories.DataBase.Entity.Room
{
    internal class RoomData :IRoomData
    {
        public uint Id { get; set; }
        public uint DormId { get; set; }
        public string NumberRoom { get; set; }
        public uint RoomArea { get; set; }
        public uint TotalNumberPlaces { get; set; }
        public uint Floor { get; set; }
        public uint NumberFreePlaces { get; set; }

        public RoomData(uint id, uint dormId, string numberRoom, uint roomArea, uint totalNumberPlaces, uint floor, uint numberFreePlaces) 
        { 
            Id = id;
            DormId = dormId;
            NumberRoom = numberRoom;
            RoomArea = roomArea;
            TotalNumberPlaces = totalNumberPlaces;
            Floor = floor;
            NumberFreePlaces = numberFreePlaces;
        }
    }
}
