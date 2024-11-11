using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfDormitories.DataBase.Entity.District
{
    public interface IDistrictData
    {
        uint Id { get; set; }
        string Name { get; set; }
    }
}
