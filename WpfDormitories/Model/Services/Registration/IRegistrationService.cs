using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfTest.Model.Services.Registration
{
    public interface IRegistrationService
    {
        bool TryRegistration(string login,string password);
    }
}
