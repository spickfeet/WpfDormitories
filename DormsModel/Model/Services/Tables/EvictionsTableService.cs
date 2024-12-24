using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfDormitories.DataBase.Entity.Child.ParentsAndChildren;
using WpfDormitories.DataBase.Entity.Child;
using WpfDormitories.DataBase.Entity.Dorm;
using WpfDormitories.DataBase.Entity.Resident;
using WpfDormitories.DataBase.Entity.Room;
using WpfDormitories.DataBase.Entity.Street;
using WpfDormitories.DataBase;
using WpfDormitories.TemporarySolutions;
using WpfDormitories.DataBase.Entity.Eviction;

namespace WpfDormitories.Model.Services.Tables
{
    public class EvictionsTableService : ITableService
    {
        private List<IEvictionData> _evictions;
        public Action<DataRow> OnEdit { get; set; }
        public Action OnAdd { get; set; }

        /// <summary>
        /// Вызвать событие для изменения объекта.
        /// </summary>
        /// <param name="index"></param>
        public void Edit(int index)
        {
            OnEdit.Invoke(DataTableParser.ToDataTable(_evictions).Rows[index]);
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

           _evictions = DataManager.GetInstance().EvictionsRepository.Read().ToList();

            DataTable res = CreateDataTable();

            List<IEvictionData> resEviction = new();

            foreach (IEvictionData eviction in _evictions)
            {
                if (eviction.PersonDocuments.RegistrationNumber.Contains(text) ||
                    eviction.PersonDocuments.Passport.FullName.Name.ToUpper().Contains(text) ||
                    eviction.PersonDocuments.Passport.FullName.Surname.ToUpper().Contains(text) ||
                    eviction.PersonDocuments.Passport.FullName.Patronymic.ToUpper().Contains(text) ||
                    eviction.PersonDocuments.Passport.Gender.ToUpper().Contains(text) ||
                    eviction.PersonDocuments.Passport.Series.ToUpper().Contains(text) ||
                    eviction.PersonDocuments.Passport.Number.ToUpper().Contains(text) ||
                    eviction.PersonDocuments.Passport.WhoGave.ToUpper().Contains(text))
                {
                    resEviction.Add(eviction);
                }
            }
            _evictions = resEviction;

            foreach (IEvictionData eviction in _evictions)
            {
                res.Rows.Add(eviction.PersonDocuments.RegistrationNumber,
                    eviction.PersonDocuments.Passport.FullName.Surname,
                    eviction.PersonDocuments.Passport.FullName.Name,
                    eviction.PersonDocuments.Passport.FullName.Patronymic,
                    eviction.PersonDocuments.Passport.Gender,
                    eviction.PersonDocuments.Passport.DateOfBirth.ToString("yyyy-MM-dd"),
                    eviction.PersonDocuments.Passport.Series,
                    eviction.PersonDocuments.Passport.Number,
                    eviction.PersonDocuments.Passport.DateOfIssue.ToString("yyyy-MM-dd"),
                    eviction.PersonDocuments.Passport.WhoGave,
                    eviction.Date.ToString("yyyy-MM-dd"));
            }
            return res;
        }

        /// <summary>
        /// Прочитать все объекты.
        /// </summary>
        /// <returns></returns>
        public virtual DataTable Read()
        {
            _evictions = DataManager.GetInstance().EvictionsRepository.Read().ToList();

            DataTable res = CreateDataTable();

            foreach (IEvictionData eviction in _evictions)
            {
                res.Rows.Add(eviction.PersonDocuments.RegistrationNumber,
                    eviction.PersonDocuments.Passport.FullName.Surname,
                    eviction.PersonDocuments.Passport.FullName.Name,
                    eviction.PersonDocuments.Passport.FullName.Patronymic,
                    eviction.PersonDocuments.Passport.Gender,
                    eviction.PersonDocuments.Passport.DateOfBirth.ToString("yyyy-MM-dd"),
                    eviction.PersonDocuments.Passport.Series,
                    eviction.PersonDocuments.Passport.Number,
                    eviction.PersonDocuments.Passport.DateOfIssue.ToString("yyyy-MM-dd"),
                    eviction.PersonDocuments.Passport.WhoGave,
                    eviction.Date.ToString("yyyy-MM-dd"));
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
            res.Columns.Add("Регистрационный номер");
            res.Columns.Add("Фамилия");
            res.Columns.Add("Имя");
            res.Columns.Add("Отчество");
            res.Columns.Add("Пол");
            res.Columns.Add("Дата рождения");
            res.Columns.Add("Серия паспорта");
            res.Columns.Add("Номер паспорта");
            res.Columns.Add("Дата выдачи паспорта");
            res.Columns.Add("Кем выдан паспорт");
            res.Columns.Add("Дата выселения");
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
            DataManager.GetInstance().EvictionsRepository.Delete(_evictions[index]);
            _evictions.Remove(_evictions[index]);
        }

        /// <summary>
        /// Получить объект по индексу.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public DataRow GetByIndex(int index)
        {
            return DataTableParser.ToDataTable(_evictions).Rows[index];
        }
    }
}
