using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfDormitories.DataBase.Entity.Dorm
{
    internal class DormData : IDormData
    {
        public uint Id { get; set; }
        public uint StreetId { get; set; }
        public uint DistrictId { get; set; }
        public string DormNumber { get; set; }
        public string HouseNumber { get; set; }
        public uint NumberRooms { get; set; }
        public uint NumberPlace { get; set; }

        public DormData(uint id, uint streetId, uint districtId, string dormNumber, string houseNumber, uint numberRooms, uint numberPlace) 
        {
            Id = id;
            StreetId = streetId;
            DistrictId = districtId;
            HouseNumber = houseNumber;
            NumberRooms = numberRooms;
            NumberPlace = numberPlace;
            DormNumber = dormNumber;
        }
    }
}
