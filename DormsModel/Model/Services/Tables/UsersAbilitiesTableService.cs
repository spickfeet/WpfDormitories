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


        /// <summary>
        /// Найти объекты по тексту.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public DataTable FindAll(string text)
        {
            _usersAbilities = DataManager.GetInstance().UsersAbilitiesRepository.Read().ToList().FindAll(item => item.UserId != 2);
            
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

        /// <summary>
        /// Прочитать все объекты.
        /// </summary>
        /// <returns></returns>
        public DataTable Read()
        {
            _usersAbilities = DataManager.GetInstance().UsersAbilitiesRepository.Read().ToList().FindAll(item => item.UserId != 2); ;
            DataTable res = CreateDataTable();
            foreach (IUserAbilitiesData userAbility in _usersAbilities)
            {
                IMenuElementData menuElement = _menuElements.Find(item => item.Id == userAbility.MenuElementId);
                res.Rows.Add(_users.Find(item => item.Id == userAbility.UserId).User.Login, menuElement.Name, userAbility.R, userAbility.W, userAbility.E,userAbility.D);
            }
            return res;
        }

        /// <summary>
        /// Создать таблицу.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Вызвать событие для добавления объекта.
        /// </summary>
        /// <param></param>
        public void Add()
        {
            OnAdd.Invoke();
        }

        /// <summary>
        /// Удалить объект по индексу.
        /// </summary>
        /// <param name="index"></param>
        public void Delete(int index)
        {
            DataManager.GetInstance().UsersAbilitiesRepository.Delete(_usersAbilities[index]);
            _usersAbilities.Remove(_usersAbilities[index]);
        }

        /// <summary>
        /// Получить объект по индексу.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public DataRow GetByIndex(int index)
        {
            return DataTableParser.ToDataTable<IUserAbilitiesData>(_usersAbilities).Rows[index];
        }
    }
}
