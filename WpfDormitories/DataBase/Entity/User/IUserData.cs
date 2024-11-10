using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfDormitories.DataBase.Entity.User
{
    public interface IUserData
    {
        uint Id { get; set; }
        string Login { get; set; }
        string Password { get; set; }
    }
}
