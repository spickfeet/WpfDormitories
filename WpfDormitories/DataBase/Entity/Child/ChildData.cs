using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfDormitories.Model.FullName;

namespace WpfDormitories.DataBase.Entity.Child
{
    public class ChildData : IChildData
    {
        private uint _id;
        private string _gender;
        private DateOnly _dateOfBirth;
        private IFullName _fullName;
        public uint Id 
        { 
            get => _id; 
            set => _id = value; 
        }
        public string Gender 
        { 
            get => _gender; 
            set => _gender = value; 
        }
        public DateOnly DateOfBirth 
        {
            get => _dateOfBirth;
            set => _dateOfBirth = value; 
        }
        public IFullName FullName 
        { 
            get => _fullName; 
            set => _fullName = value;
        }

        public ChildData(uint id, string gender, DateOnly dateOfBirth, IFullName fullName)
        {
            Id = id;
            Gender = gender;
            DateOfBirth = dateOfBirth;
            FullName = fullName;
        }
    }
}
