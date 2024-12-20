using Microsoft.Office.Interop.Word;
using System.Data;
using System.IO;
using System.Windows;
using WpfDormitories.DataBase;
using WpfDormitories.DataBase.Entity.Contract;
using WpfDormitories.DataBase.Entity.Dorm;
using WpfDormitories.DataBase.Entity.Resident;
using WpfDormitories.DataBase.Entity.Room;
using WpfDormitories.DataBase.Entity.Street;

namespace DormsModel.Model
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
                Table table = word.Application.ActiveDocument.Tables.Add(word.Selection.Range, dataTable.Rows.Count + 1, dataTable.Columns.Count, Type.Missing, Type.Missing);
                table.Borders.OutsideLineStyle = WdLineStyle.wdLineStyleSingle;
                table.Borders.InsideLineStyle = WdLineStyle.wdLineStyleSingle;

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

        public static void ExportContract(IContractData contract)
        {
            Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();

            string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Договор.docx");

            Document doc = wordApp.Documents.Open(fullPath);

            doc.Content.Select(); // Выделяем всё содержимое документа
            wordApp.Selection.Copy(); // Копируем выделенное в буфер обмена
            doc.Close();

            doc = wordApp.Documents.Add(Type.Missing);
            Microsoft.Office.Interop.Word.Range range = doc.Content;
            range.Select(); // Выбираем весь диапазон (или можете выбрать конкретное место для вставки)
            wordApp.Selection.Paste(); //

            FindAndReplace(wordApp, "{document_name}", contract.Name);
            FindAndReplace(wordApp, "{document_number}", contract.DocumentNumber);

            string dayStartAction = contract.StartAction.Day.ToString();
            string monthStartAction = contract.StartAction.Month.ToString();
            string yearStartAction = contract.StartAction.Year.ToString();

            FindAndReplace(wordApp, "day_start_action}", dayStartAction);
            FindAndReplace(wordApp, "{month_start_action}", monthStartAction);
            FindAndReplace(wordApp, "{year_start_action}", yearStartAction);

            List<IResidentData> residents = DataManager.GetInstance().ResidentsRepository.Read().ToList().FindAll(item => item.ContractId == contract.Id);
            IRoomData room = DataManager.GetInstance().RoomsRepository.Read().ToList().Find(item => item.Id == residents[0].RoomId);
            IDormData dorm = DataManager.GetInstance().DormsRepository.Read().ToList().Find(item => item.Id == room.DormId);
            IStreetData street = DataManager.GetInstance().StreetsRepository.Read().ToList().Find(item => item.Id == dorm.StreetId);

            FindAndReplace(wordApp, "{count_people}", residents.Count);
            FindAndReplace(wordApp, "{number_room}", room.NumberRoom);
            FindAndReplace(wordApp, "{dorm_street}", street.Name);
            FindAndReplace(wordApp, "{dorm_house_number}", dorm.HouseNumber);

            float price = 0;

            FindAndReplace(wordApp, "{contract_who_gave}", contract.WhoGave);

            FindAndReplace(wordApp, "{resident_surname}", residents[0].PersonDocuments.Passport.FullName.Surname);
            FindAndReplace(wordApp, "{resident_name}", residents[0].PersonDocuments.Passport.FullName.Name);
            FindAndReplace(wordApp, "{resident_patronymic}", residents[0].PersonDocuments.Passport.FullName.Patronymic);
            FindAndReplace(wordApp, "{resident_series_passport}", residents[0].PersonDocuments.Passport.Series);
            FindAndReplace(wordApp, "{resident_number_passport}", residents[0].PersonDocuments.Passport.Number);
            FindAndReplace(wordApp, "{resident_passport_who_gave}", residents[0].PersonDocuments.Passport.WhoGave);
            FindAndReplace(wordApp, "{resident_passport_date_of_issue}", residents[0].PersonDocuments.Passport.DateOfIssue.ToString("dd.MM.yyyy"));

            System.Data.DataTable res = new();
            res.Columns.Add("Все участники");

            foreach (IResidentData resident in residents)
            {
                price += resident.Payment;
                res.Rows.Add(
                    $"Фамилия: {resident.PersonDocuments.Passport.FullName.Surname}\n" +
                    $"Имя: {resident.PersonDocuments.Passport.FullName.Name}\n" +
                    $"Отчество: {resident.PersonDocuments.Passport.FullName.Patronymic}\n" +
                    $"Паспорт: серия {resident.PersonDocuments.Passport.Series} № {resident.PersonDocuments.Passport.Number}\n" +
                    $"Кем выдан {resident.PersonDocuments.Passport.WhoGave}\n" +
                    $"Дата выдачи " + resident.PersonDocuments.Passport.DateOfIssue.ToString("dd.MM.yyyy"));
            }

            FindAndReplace(wordApp, "{price}", price);
            FindAndReplaceTable(doc, "{all_residents}", res);

            wordApp.Visible = true;
        }

        private static void FindAndReplace(Microsoft.Office.Interop.Word.Application wordApp, object findText, object replaceWithText)
        {
            Microsoft.Office.Interop.Word.Range range = wordApp.ActiveDocument.Content;
            range.Find.ClearFormatting();
            range.Find.Execute(FindText: ref findText,
                               ReplaceWith: ref replaceWithText,
                               Replace: WdReplace.wdReplaceAll);
        }

        static void FindAndReplaceTable(Document doc, string findText, System.Data.DataTable dataTable)
        {
            Microsoft.Office.Interop.Word.Range range = doc.Content;
            range.Find.ClearFormatting();
            range.Find.Execute(findText);

            if (range.Find.Found)
            {
                Table table = doc.Tables.Add(range, dataTable.Rows.Count + 1, dataTable.Columns.Count, Type.Missing, Type.Missing);

                if (dataTable.Rows.Count > 0)
                {
                    float fontSize = 14;
                    if (dataTable.Columns.Count > 8)
                    {
                        fontSize = 10;
                    }

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

                    table.Borders.OutsideLineStyle = WdLineStyle.wdLineStyleSingle;
                    table.Borders.InsideLineStyle = WdLineStyle.wdLineStyleSingle;

                    range.Text = "";
                }
            }
        }

    }
}
