using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfDormitories.Model.Services.Tables
{
    public interface ITableService
    {
        public Action<DataRow> OnEdit {  get; set; }
        void Add();
        DataTable Read();
        DataTable FindAll(string elementName);
        void Edit(int index);
        void Delete(int index);

    }
}
