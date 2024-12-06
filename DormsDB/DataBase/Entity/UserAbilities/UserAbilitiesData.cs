using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfDormitories.DataBase.Entity.UserAbilities
{
    public class UserAbilitiesData : IUserAbilitiesData
    {
        public uint Id { get; set; }
        public uint UserId { get; set; }
        public uint MenuElementId { get; set; }
        public bool R { get; set; }
        public bool W { get; set; }
        public bool E { get; set; }
        public bool D { get; set; }

        public UserAbilitiesData(uint id, uint userId, uint menuElementId, bool r, bool w, bool e, bool d)
        {
            Id = id;
            UserId = userId;
            MenuElementId = menuElementId;
            R = r;
            W = w;
            E = e;
            D = d;
        }

        public UserAbilitiesData(uint userId, uint menuElementId, bool r, bool w, bool e, bool d)
        {
            UserId = userId;
            MenuElementId = menuElementId;
            R = r;
            W = w;
            E = e;
            D = d;
        }
    }
}
