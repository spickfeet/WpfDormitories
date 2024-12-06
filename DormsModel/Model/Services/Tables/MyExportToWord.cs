using Microsoft.Office.Interop.Word;
using System.Data;
using System.Windows;

namespace WpfDormitories.Model.Services.Tables
{
    public class ExportToWord
    {
        public static void ExportTable(System.Data.DataTable dataTable)
        {
            if (dataTable.Rows.Count > 0)
            {
                float fontSize = 11; 
                Microsoft.Office.Interop.Word.Application word = new Microsoft.Office.Interop.Word.Application();
                word.Application.Documents.Add(Type.Missing);

                if (dataTable.Columns.Count > 8)
                {
                    fontSize = 8;
                    word.Application.ActiveDocument.PageSetup.Orientation = WdOrientation.wdOrientLandscape;
                }
                Microsoft.Office.Interop.Word.Table table = word.Application.ActiveDocument.Tables.Add(word.Selection.Range, dataTable.Rows.Count + 1, dataTable.Columns.Count, Type.Missing, Type.Missing);
                table.Borders.OutsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
                table.Borders.InsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;

                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    table.Cell(1, i + 1).Range.Text = dataTable.Columns[i].ColumnName;
                    table.Cell(1, i + 1).Range.Font.Size = fontSize;
                }

                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    for (int j = 0; j < dataTable.Columns.Count; j++)
                    {
                        table.Cell(i + 2, j + 1).Range.Text = dataTable.Rows[i][j].ToString();
                        table.Cell(i + 2, j + 1).Range.Font.Size = fontSize;
                    }
                }

                word.Visible = true;
            }
        }
    }
}
