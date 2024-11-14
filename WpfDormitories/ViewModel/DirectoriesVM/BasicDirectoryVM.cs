using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfDormitories.ViewModel.DirectoriesVM
{
    public abstract class BasicDirectoryVM : BasicVM, IApplicableVM
    {
        protected string _name;
        public Action OnApply { get; set; }
        public string Name
        {
            get { return _name; }
            set { Set(ref _name, value); }
        }
    }
}
