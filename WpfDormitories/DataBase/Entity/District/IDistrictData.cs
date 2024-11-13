using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfDormitories.DataBase.Entity.District
{
    public interface IDistrictData
    {
        uint Id { get; set; }

        [DisplayName("Районы")]
        string Name { get; set; }
    }
}
