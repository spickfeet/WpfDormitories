using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfDormitories.DataBase.Entity.Eviction;
using WpfDormitories.DataBase;
using System.Data;

namespace WpfDormitories.Model.Services.Tables.ExportFormattedTables
{
    public class ExportEvictionsTableService : EvictionsTableService
    {
        public virtual DataTable Read()
        {
            IList<IEvictionData> evictions = DataManager.GetInstance().EvictionsRepository.Read();

            DataTable res = CreateDataTable();

            foreach (IEvictionData eviction in evictions)
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
                    eviction.Date.ToString("yyyy-MM-dd"),
                    eviction.Reason);
            }
            return res;
        }

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
            res.Columns.Add("Причина");
            return res;
        }
    }
}
