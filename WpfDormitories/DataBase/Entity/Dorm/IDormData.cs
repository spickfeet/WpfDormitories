using System;
using System.Collections.Generic;
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
        string DormNumber { get; set; }
        string HouseNumber { get; set; }
        uint NumberRooms { get; set; }
        uint NumberPlace { get; set; }
    }
}
