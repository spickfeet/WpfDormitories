using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfDormitories.Model.FullName
{
    public class FullName : IFullName
    {
        private string _surname;
        private string _name;
        private string _patronymic;
        public string Surname { get => _surname; set => _surname = value; }
        public string Name { get => _name; set => _name =value; }
        public string Patronymic { get => _patronymic; set => _patronymic = value; }
        public FullName(string surname, string name, string patronymic)
        {
            Surname = surname;
            Name = name;
            Patronymic = patronymic;
        }
    }
}
