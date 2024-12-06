using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfDormitories.DataBase.Entity.Street
{
    public interface IStreetData
    {
        uint Id { get; set; }
        [DisplayName("Улица")]
        string Name { get; set; }
    }
}
