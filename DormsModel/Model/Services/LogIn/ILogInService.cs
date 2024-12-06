using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfDormitories.Model.Services.LogIn
{
    public interface ILogInService
    {
        public bool TryLogIn(string login, string password);
    }
}
