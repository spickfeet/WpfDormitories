using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfDormitories.Model.Services.ChangePassword
{
    public interface IPasswordChangerService
    {
        public void Change(string oldPassword, string newPassword);
    }
}
