using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfDormitories.DataBase.Entity.Room
{
    public interface IRoomData
    {
        uint Id { get; set; }
        uint DormId { get; set; }
        [DisplayName("Номер комнаты")]
        string NumberRoom { get; set; }
        [DisplayName("Площадь комнаты")]
        uint RoomArea { get; set; }
        [DisplayName("Общее количество мест")]
        uint TotalNumberPlaces { get; set; }
        [DisplayName("Этаж")]
        uint Floor { get; set; }
        [DisplayName("Количество свободных мест")]
        uint NumberFreePlaces { get; set; }
    }
}
