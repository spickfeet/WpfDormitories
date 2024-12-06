using System.Data;

namespace WpfDormitories.Model.Services.Tables
{
    public interface ITableService
    {
        public Action<DataRow> OnEdit {  get; set; }
        Action OnAdd { get; set; }
        void Add();
        DataTable Read();
        DataTable FindAll(string elementName);
        void Edit(int index);
        void Delete(int index);
        DataRow GetByIndex(int index);
    }
}
