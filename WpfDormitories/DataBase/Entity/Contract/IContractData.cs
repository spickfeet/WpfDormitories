using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfDormitories.DataBase.Entity.Contract
{
    public interface IContractData
    {
        uint Id { get; set; }
        uint DocumentNumber { get; set; }
        string Name { get; set; }
        string WhoGave { get; set; }
        DateOnly StartAction { get; set; }
        string Comment { get; set; }
    }
}
