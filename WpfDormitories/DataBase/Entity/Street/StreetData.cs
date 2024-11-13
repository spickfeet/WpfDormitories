using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfDormitories.DataBase.Entity.Street
{
    public class StreetData : IStreetData
    {
        public uint Id { get; set; }
        public string Name { get; set; }

        public StreetData(uint id, string name) 
        {
            Id = id;
            Name = name;
        }
    }
}
