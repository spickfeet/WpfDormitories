using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfDormitories.Model.FullName
{
    public interface IFullName
    {
        string Surname { get; set; }
        string Name { get; set; }
        string Patronymic { get; set; }
    }
}
