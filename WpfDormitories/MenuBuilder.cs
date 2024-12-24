using System.Reflection;
using System.Windows.Controls;
using System.Windows;
using WpfDormitories.DataBase.Entity.MenuElement;
using WpfDormitories.Views;
using System.Data;
using WpfDormitories.DataBase;
using WpfDormitories.DataBase.Entity.UserAbilities;
using System.Windows.Data;
using System.Diagnostics;
using System.Linq;

namespace WpfDormitories
{
    /// <summary>
    /// Класс для создания меню.
    /// </summary>
    public class MenuBuilder
    {
        private MainWindow _mainWindow;
        private List<IMenuElementData> _elements;
        private IList<IUserAbilitiesData> _userAbilities;
        public MenuBuilder(MainWindow main)
        {
            _mainWindow = main;
            _elements = new List<IMenuElementData>();
            _userAbilities = new List<IUserAbilitiesData>();
            GetUserAbilities();
        }

        /// <summary>
        /// Получить права текущего пользователя.
        /// </summary>
        private void GetUserAbilities()
        {
            IList<IUserAbilitiesData> allUsersAbilities = DataManager.GetInstance().UsersAbilitiesRepository.Read();
            foreach (var userAbilities in allUsersAbilities)
            {
                if(userAbilities.UserId == DataManager.GetInstance().CurrentUser.Id)
                {
                    _userAbilities.Add(userAbilities);
                }
            }
        }

        /// <summary>
        /// Метод для создания меню.
        /// </summary>
        /// <returns></returns>
        public Menu BuildMenu()
        {
            _elements = DataManager.GetInstance().MenuElementsRepository.Read().ToList<IMenuElementData>();


            Menu mainMenu = new Menu();


            var topLevelItems = _elements.FindAll(item => item.ParentId == 0);
            topLevelItems.Sort((b1, b2) => b1.Order.CompareTo(b2.Order));
            foreach (var topLevelItem in topLevelItems)
            {
                MenuItem menuItem = CreateMenuItem(topLevelItem);
                mainMenu.Items.Add(menuItem);
                AddSubMenuItems(menuItem, topLevelItem.Id);

            }
            return mainMenu;
        }

        /// <summary>
        /// Добавить подэлемент меню.
        /// </summary>
        /// <param name="parentMenuItem"></param>
        /// <param name="parentId"></param>
        private void AddSubMenuItems(MenuItem parentMenuItem, uint parentId)
        {
            var subMenuItems = _elements.FindAll(item => item.ParentId == parentId);
            subMenuItems.Sort((b1, b2) => b1.Order.CompareTo(b2.Order));
            foreach (var subMenuItem in subMenuItems)
            {
                MenuItem menuItem = CreateMenuItem(subMenuItem);
                parentMenuItem.Items.Add(menuItem);
                AddSubMenuItems(menuItem, subMenuItem.Id);
            }
        }

        /// <summary>
        /// Добавить элемент меню.
        /// </summary>
        /// <param name="menuItemData"></param>
        /// <returns></returns>
        private MenuItem CreateMenuItem(IMenuElementData menuItemData)
        {

            MenuItem menuItem = new MenuItem
            {
                Header = menuItemData.Name,
                Tag = menuItemData
            };
            if (!string.IsNullOrEmpty(menuItemData.FuncName))
            {
                foreach (IUserAbilitiesData _userAbilitiesItem in _userAbilities)
                {
                    if(_userAbilitiesItem.MenuElementId == menuItemData.Id)
                    {
                        if (_userAbilitiesItem.R == true)
                        {
                            DependencyProperty commProp = MenuItem.CommandProperty;
                            if (!BindingOperations.IsDataBound(menuItem, commProp))
                            {
                                Binding binding = new Binding(menuItemData.FuncName);
                                BindingOperations.SetBinding(menuItem, commProp, binding);
                            }
                        }
                        else
                        {
                            menuItem.Visibility = Visibility.Collapsed;
                        }
                        break;
                    }
                }

                
            }
            return menuItem;
        }
    }
}
