using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfDormitories.ViewModel
{
    public interface IApplicableVM
    {
        Action OnApply { get; set; }
        bool ConfirmApplyStatus { get; set; }
    }
}
