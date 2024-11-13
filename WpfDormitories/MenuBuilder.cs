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

namespace WpfDormitories
{
    public class MenuBuilder
    {
        public Menu menu;
        string bdName;
        int userId;

        MainWindow MainWindow { get; set; }
        List<IMenuElementData> Items;
        private ICollection<IUserAbilitiesData> _userAbilities;
        private string connectionString;
        public MenuBuilder(MainWindow main)
        {
            MainWindow = main;
            Items = new List<IMenuElementData>();
            _userAbilities = new List<IUserAbilitiesData>();
            GetUserAbilities();
        }

        private void GetUserAbilities()
        {
            ICollection<IUserAbilitiesData> allUsersAbilities = DataManager.GetInstance().UsersAbilitiesRepository.Read();
            foreach (var userAbilities in allUsersAbilities)
            {
                if(userAbilities.UserId == DataManager.GetInstance().CurrentUser.Id)
                {
                    _userAbilities.Add(userAbilities);
                }
            }
        }

        private void ReadBd()
        {

            string query = "SELECT * FROM menu_elements";

            DataTable dt = DormitorySQLConnection.GetInstance().GetData(query);

            foreach (DataRow row in dt.Rows)
            {
                IMenuElementData menuItem = new MenuElementData(uint.Parse(row[0].ToString()), uint.Parse(row[1].ToString()), row[2].ToString(),
                    row[3]?.ToString(), row[4]?.ToString(), uint.Parse(row[5].ToString()));
                Items.Add(menuItem);

            } 
        }
        public Menu BuildMenu()
        {
            ReadBd();


            Menu mainMenu = new Menu();


            var topLevelItems = Items.FindAll(item => item.ParentId == 0);
            topLevelItems.Sort((b1, b2) => b1.Order.CompareTo(b2.Order));
            foreach (var topLevelItem in topLevelItems)
            {
                MenuItem menuItem = CreateMenuItem(topLevelItem);
                mainMenu.Items.Add(menuItem);
                AddSubMenuItems(menuItem, topLevelItem.Id);

            }
            return mainMenu;
        }
        private void AddSubMenuItems(MenuItem parentMenuItem, uint parentId)
        {

            var subMenuItems = Items.FindAll(item => item.ParentId == parentId);
            subMenuItems.Sort((b1, b2) => b1.Order.CompareTo(b2.Order));
            foreach (var subMenuItem in subMenuItems)
            {
                MenuItem menuItem = CreateMenuItem(subMenuItem);
                parentMenuItem.Items.Add(menuItem);
                AddSubMenuItems(menuItem, subMenuItem.Id);
            }
        }

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
                            //Binding myBinding = new Binding();
                            //myBinding.Source = MainWindow.DataContext;
                            //myBinding.Path = new PropertyPath(menuItemData.FuncName);
                            //myBinding.Mode = BindingMode.TwoWay;
                            //myBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;

                            DependencyProperty commProp = MenuItem.CommandProperty;
                            if (!BindingOperations.IsDataBound(menuItem, commProp))
                            {
                                Binding binding = new Binding(menuItemData.FuncName);
                                BindingOperations.SetBinding(menuItem, commProp, binding);
                            }
                        }
                        else
                        {
                            menuItem.Click += (sender, e) =>
                            {
                                MessageBox.Show("У данного пользователя нету прав доступа.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                            };
                        }
                        break;
                    }
                }

                
            }
            return menuItem;
        }
    }
}
