using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfDormitories.DataBase.Entity.Dorm
{
    public interface IDormData
    {
        uint Id { get; set; }
        uint StreetId { get; set; }
        uint DistrictId { get; set; }
        [DisplayName("Номер общежития")]
        string DormNumber { get; set; }
        [DisplayName("Номер дома")]
        string HouseNumber { get; set; }
        [DisplayName("Количество комнат")]
        uint NumberRooms { get; set; }
        [DisplayName("Количество мест")]
        uint NumberPlace { get; set; }
    }
}
