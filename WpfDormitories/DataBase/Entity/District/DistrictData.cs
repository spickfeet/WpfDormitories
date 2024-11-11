using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfDormitories.DataBase.Entity.District
{
    public class DistrictData : IDistrictData
    {
        public uint Id { get; set; }
        public string Name { get; set; }

        public DistrictData(uint id, string name) 
        {
            Id = id;
            Name = name;
        }
    }
}
