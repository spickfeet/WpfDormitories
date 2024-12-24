using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using WpfDormitories.DataBase.Entity.Street;
using WpfDormitories.DataBase;
using WpfDormitories.TemporarySolutions;
using WpfDormitories.DataBase.Entity.Dorm;

namespace WpfDormitories.Model.Services.Tables
{
    public class StreetsTableService : ITableService
    {
        private IList<IStreetData> _streetsData;

        public Action<DataRow> OnEdit { get; set; }
        public Action OnAdd { get; set; }

        /// <summary>
        /// Вызвать событие для изменения объекта.
        /// </summary>
        /// <param name="index"></param>
        public void Edit(int index)
        {
            OnEdit.Invoke(DataTableParser.ToDataTable<IStreetData>(_streetsData).Rows[index]);
        }

        /// <summary>
        /// Найти объекты по тексту.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public DataTable FindAll(string text)
        {
            _streetsData = DataManager.GetInstance().StreetsRepository.Read();
            if (string.IsNullOrEmpty(text))
            {
                return Read();
            }

            IList<IStreetData> res = new List<IStreetData>();
            foreach (IStreetData streetData in _streetsData)
            {
                if (streetData.Name.ToUpper().Contains(text.ToUpper()))
                {
                    res.Add(streetData);
                }
            }
            _streetsData = res;
            DataTable dt = DataTableParser.ToDataTable<IStreetData>(res);
            dt.Columns.Remove(dt.Columns[0]);
            return dt;
        }

        /// <summary>
        /// Прочитать все объекты.
        /// </summary>
        /// <returns></returns>
        public DataTable Read()
        {
            _streetsData = DataManager.GetInstance().StreetsRepository.Read();
            DataTable dt = DataTableParser.ToDataTable<IStreetData>(_streetsData);
            dt.Columns.Remove(dt.Columns[0]);
            return dt;
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
            DataManager.GetInstance().StreetsRepository.Delete(_streetsData[index]);
            _streetsData.Remove(_streetsData[index]);
        }

        /// <summary>
        /// Получить объект по индексу.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public DataRow GetByIndex(int index)
        {
            return DataTableParser.ToDataTable<IStreetData>(_streetsData).Rows[index];
        }
    }
}
