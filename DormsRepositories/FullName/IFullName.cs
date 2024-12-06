using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfDormitories.Model.FullName
{
    public interface IFullName
    {
        [DisplayName("Фамилия")]
        string Surname { get; set; }
        [DisplayName("Имя")]
        string Name { get; set; }
        [DisplayName("Отчество")]
        string? Patronymic { get; set; }
    }
}
