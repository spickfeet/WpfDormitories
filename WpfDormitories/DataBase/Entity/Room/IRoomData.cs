using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfDormitories.DataBase.Entity.Room
{
    public interface IRoomData
    {
        uint Id { get; set; }
        uint DormId { get; set; }
        string NumberRoom { get; set; }
        uint RoomArea { get; set; }
        uint TotalNumberPlaces { get; set; }
        uint Floor { get; set; }
        uint NumberFreePlaces { get; set; }
    }
}
