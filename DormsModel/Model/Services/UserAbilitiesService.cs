using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using WpfDormitories.DataBase;
using WpfDormitories.DataBase.Entity.MenuElement;
using WpfDormitories.DataBase.Entity.UserAbilities;

namespace WpfDormitories.Model.Services
{
    public class UserAbilitiesService
    {
        private IList<IUserAbilitiesData> _userAbilities;
        private IList<IMenuElementData> _menuElements;
        public UserAbilitiesService()
        {
            _menuElements = DataManager.GetInstance().MenuElementsRepository.Read();
            _userAbilities = new List<IUserAbilitiesData>();
            IList<IUserAbilitiesData> usersAbilities = DataManager.GetInstance().UsersAbilitiesRepository.Read();

            foreach (IUserAbilitiesData abilities in usersAbilities)
            {
                if (abilities.UserId == DataManager.GetInstance().CurrentUser.Id)
                {
                    _userAbilities.Add(abilities);
                }
            }
        }
        public IUserAbilitiesData GetUserAbilitiesByFuncName(string funcName)
        {
            foreach (IMenuElementData menuElement in _menuElements)
            {
                if(menuElement.FuncName == funcName)
                {
                    foreach (IUserAbilitiesData ability in _userAbilities)
                    {
                        if(ability.MenuElementId == menuElement.Id)
                            return ability;
                    }
                    throw new ArgumentException($"Для пользователя {DataManager.GetInstance().CurrentUser.User.Login} отсутствуют права доступа к пункту меню {menuElement.Name}");
                }
            }
            throw new ArgumentException($"Не найден пункт меню с функцией {funcName}");
        }

    }
}
