using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfDormitories.DataBase.Entity.MenuElement;
using WpfDormitories.DataBase.Entity.User;
using WpfDormitories.DataBase;

namespace WpfDormitories.ViewModel.UserAbilitiesVM
{
    public abstract class BasicUserAbilitiesVM : BasicVM, IApplicableVM
    {
        protected IList<IUserData> _users;
        protected List<string> _usersLogins;
        protected IList<IMenuElementData> _elementsMenu;
        protected List<string> _elementsMenuNames;

        protected int _selectedMenuElementIndex;
        protected int _selectedUserIndex;

        protected bool _r;
        protected bool _w;
        protected bool _e;
        protected bool _d;
        public Action OnApply { get; set; }

        public BasicUserAbilitiesVM()
        {
            _usersLogins = new();
            _elementsMenuNames = new();
            _users = DataManager.GetInstance().UsersRepository.Read();
            _elementsMenu = DataManager.GetInstance().MenuElementsRepository.Read().
                ToList<IMenuElementData>().FindAll(item => !string.IsNullOrEmpty(item.FuncName));
            foreach (IUserData user in _users)
            {
                _usersLogins.Add(user.User.Login);
            }
            foreach (IMenuElementData menuElement in _elementsMenu)
            {
                _elementsMenuNames.Add(menuElement.Name);
            }
            _selectedUserIndex = -1;
            _selectedMenuElementIndex = -1;
        }

        public int SelectedMenuElementIndex
        {
            get { return _selectedMenuElementIndex; }
            set { Set<int>(ref _selectedMenuElementIndex, value); }
        }

        public int SelectedUserIndex
        {
            get { return _selectedUserIndex; }
            set { Set<int>(ref _selectedUserIndex, value); }
        }

        public List<string> UsersLogins
        {
            get { return _usersLogins; }
            set { Set<List<string>>(ref _usersLogins, value); }
        }

        public List<string> MenuElements
        {
            get { return _elementsMenuNames; }
            set { Set<List<string>>(ref _elementsMenuNames, value); }
        }
        public bool R
        {
            get { return _r; }
            set { Set<bool>(ref _r, value); }
        }
        public bool W
        {
            get { return _w; }
            set { Set<bool>(ref _w, value); }
        }
        public bool E
        {
            get { return _e; }
            set { Set<bool>(ref _e, value); }
        }
        public bool D
        {
            get { return _d; }
            set { Set<bool>(ref _d, value); }
        }
    }
}
