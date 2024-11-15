using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfDormitories.TemporarySolutions
{
    public static class DataTableParser
    {
        public static DataTable ToDataTable<T>(IList<T> list)
        {
            var props = TypeDescriptor.GetProperties(typeof(T));
            var table = new DataTable();
            foreach (PropertyDescriptor prop in props)
            {
                if (prop.DisplayName != null)
                {
                    table.Columns.Add(prop.DisplayName, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
                }
            }
            foreach (T item in list)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in props)
                {
                    if (prop.DisplayName != null)
                    {
                        row[prop.DisplayName] = prop.GetValue(item) ?? DBNull.Value;
                    }
                }
                table.Rows.Add(row);
            }
            return table;
        }
    }
}
