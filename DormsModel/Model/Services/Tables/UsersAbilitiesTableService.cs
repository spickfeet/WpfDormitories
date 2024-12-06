using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfDormitories.DataBase;
using WpfDormitories.DataBase.Entity.Dorm;
using WpfDormitories.DataBase.Entity.MenuElement;
using WpfDormitories.DataBase.Entity.Street;
using WpfDormitories.DataBase.Entity.User;
using WpfDormitories.DataBase.Entity.UserAbilities;
using WpfDormitories.TemporarySolutions;

namespace WpfDormitories.Model.Services.Tables
{
    public class UsersAbilitiesTableService : ITableService
    {
        private List<IUserAbilitiesData> _usersAbilities;
        private List<IMenuElementData> _menuElements;
        private List<IUserData> _users;
        public Action<DataRow> OnEdit { get; set; }
        public Action OnAdd { get; set; }

        public UsersAbilitiesTableService()
        {
            _users = DataManager.GetInstance().UsersRepository.Read().ToList<IUserData>();
            _menuElements = DataManager.GetInstance().MenuElementsRepository.Read().ToList<IMenuElementData>();
        }
        public void Edit(int index)
        {
            OnEdit.Invoke(DataTableParser.ToDataTable<IUserAbilitiesData>(_usersAbilities).Rows[index]);
        }
        public DataTable FindAll(string text)
        {
            _usersAbilities = DataManager.GetInstance().UsersAbilitiesRepository.Read().ToList<IUserAbilitiesData>();
            
            if (string.IsNullOrEmpty(text))
            {
                return Read();
            }
            DataTable res = CreateDataTable();

            List<IUserAbilitiesData> resAbility = new List<IUserAbilitiesData>();

            foreach (IUserAbilitiesData userAbility in _usersAbilities)
            {
                IMenuElementData menuElement = _menuElements.Find(item => item.Id == userAbility.MenuElementId);
                if (menuElement != null) 
                {
                    if (menuElement.Name.ToUpper().Contains(text.ToUpper()) || _users.Find(item => item.Id == userAbility.UserId).User.Login.ToUpper().Contains(text.ToUpper()))
                    {
                        resAbility.Add(userAbility);
                    }
                }
            }
            _usersAbilities = resAbility;

            foreach (IUserAbilitiesData userAbility in resAbility)
            {
                IMenuElementData menuElement = _menuElements.Find(item => item.Id == userAbility.MenuElementId);
                res.Rows.Add(_users.Find(item => item.Id == userAbility.UserId).User.Login, menuElement.Name, userAbility.R, userAbility.W, userAbility.E, userAbility.D);
            }
            return res;
        }

        public DataTable Read()
        {
            _usersAbilities = DataManager.GetInstance().UsersAbilitiesRepository.Read().ToList<IUserAbilitiesData>();
            DataTable res = CreateDataTable();
            foreach (IUserAbilitiesData userAbility in _usersAbilities)
            {
                IMenuElementData menuElement = _menuElements.Find(item => item.Id == userAbility.MenuElementId);
                res.Rows.Add(_users.Find(item => item.Id == userAbility.UserId).User.Login, menuElement.Name, userAbility.R, userAbility.W, userAbility.E,userAbility.D);
            }
            return res;
        }

        private DataTable CreateDataTable()
        {
            DataTable res = new();
            res.Columns.Add("Пользователь");
            res.Columns.Add("Наименование пункта");
            res.Columns.Add("R", true.GetType());
            res.Columns.Add("W", true.GetType());
            res.Columns.Add("E", true.GetType());
            res.Columns.Add("D", true.GetType());
            return res;
        }

        public void Add()
        {
            OnAdd.Invoke();
        }

        public void Delete(int index)
        {
            DataManager.GetInstance().UsersAbilitiesRepository.Delete(_usersAbilities[index]);
            _usersAbilities.Remove(_usersAbilities[index]);
        }
        public DataRow GetByIndex(int index)
        {
            return DataTableParser.ToDataTable<IUserAbilitiesData>(_usersAbilities).Rows[index];
        }
    }
}
