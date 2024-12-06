using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfDormitories.DataBase.Entity.Contract;
using WpfDormitories.DataBase;

namespace WpfDormitories.Model.Services.Tables.ExportFormattedTables
{
    class ExportContractsTableService : ContractsTableService
    {
        public override DataTable Read()
        {
            IList<IContractData> contracts = DataManager.GetInstance().ContractsRepository.Read();
            DataTable res = CreateDataTable();
            foreach (IContractData contract in contracts)
            {
                res.Rows.Add(contract.DocumentNumber, contract.Name, contract.WhoGave, contract.StartAction.ToString("yyyy-MM-dd"), contract.Comment);
            }
            return res;
        }

        private DataTable CreateDataTable()
        {
            DataTable res = new();
            res.Columns.Add("Номер документа");
            res.Columns.Add("Название документа");
            res.Columns.Add("Кем выдан");
            res.Columns.Add("Начало действия");
            res.Columns.Add("Комментарий");
            return res;
        }
    }
}
