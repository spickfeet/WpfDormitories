using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfDormitories.DataBase.Entity.Child.ParentsAndChildren;
using WpfDormitories.DataBase.Entity.Child;
using WpfDormitories.DataBase.Entity.Contract;
using WpfDormitories.DataBase.Entity.Inventory;
using WpfDormitories.DataBase.Entity.Resident;
using WpfDormitories.DataBase.Entity.Room;
using WpfDormitories.DataBase;
using WpfDormitories.TemporarySolutions;

namespace WpfDormitories.Model.Services.Tables
{
    /// <summary>
    /// Сервис для работы с таблицей дети.
    /// </summary>
    public class ChildrenTableService : ITableService
    {
        private List<IChildData> _children;
        public Action<DataRow> OnEdit { get; set; }
        public Action OnAdd { get; set; }

        /// <summary>
        /// Вызвать событие для изменения объекта.
        /// </summary>
        /// <param name="index"></param>
        public void Edit(int index)
        {
            OnEdit.Invoke(GetByIndex(index));
        }

        /// <summary>
        /// Найти объекты по тексту.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public DataTable FindAll(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return Read();
            }

            text = text.ToUpper();

            _children = DataManager.GetInstance().ChildrenRepository.Read().ToList();
            DataTable res = CreateDataTable();

            List<IChildData> resChildren = new();

            foreach (IChildData child in _children)
            {
                if (child.FullName.Surname.ToUpper().Contains(text) || child.FullName.Name.ToUpper().Contains(text) ||
                    child.FullName.Patronymic.ToUpper().Contains(text) || child.Gender.ToUpper().Contains(text))
                {
                    resChildren.Add(child);
                }
            }
            _children = resChildren;

            foreach (IChildData child in _children)
            {
                res.Rows.Add(child.FullName.Surname, child.FullName.Name, child.FullName.Patronymic, child.DateOfBirth.ToString("yyyy-MM-dd"), child.Gender);
            }
            return res;
        }

        /// <summary>
        /// Прочитать все объекты.
        /// </summary>
        /// <returns></returns>
        public DataTable Read()
        {
            _children = DataManager.GetInstance().ChildrenRepository.Read().ToList();

            DataTable res = CreateDataTable();

            foreach (IChildData child in _children)
            {
                res.Rows.Add(child.FullName.Surname, child.FullName.Name, child.FullName.Patronymic, child.DateOfBirth.ToString("yyyy-MM-dd"), child.Gender);
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
            res.Columns.Add("Фамилия");
            res.Columns.Add("Имя");
            res.Columns.Add("Отчество");
            res.Columns.Add("Дата рожденья");
            res.Columns.Add("Пол");
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
            List<IParentsAndChildrenData> parentsAndChildren = DataManager.GetInstance().ParentsAndChildrenRepository.Read().ToList().FindAll(item => item.ChildId == _children[index].Id);

            foreach (IParentsAndChildrenData parentAndChild in parentsAndChildren)
            {
                DataManager.GetInstance().ParentsAndChildrenRepository.Delete(parentAndChild);
            }
            DataManager.GetInstance().ChildrenRepository.Delete(_children[index]);
            _children.Remove(_children[index]);
        }

        /// <summary>
        /// Получить объект по индексу.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public DataRow GetByIndex(int index)
        {
            return DataTableParser.ToDataTable(_children).Rows[index];
        }
    }
}
