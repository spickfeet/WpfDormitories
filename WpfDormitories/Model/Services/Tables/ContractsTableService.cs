using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfDormitories.DataBase.Entity.Child.ParentsAndChildren;
using WpfDormitories.DataBase.Entity.Child;
using WpfDormitories.DataBase.Entity.Contract;
using WpfDormitories.DataBase.Entity.District;
using WpfDormitories.DataBase.Entity.Dorm;
using WpfDormitories.DataBase.Entity.Inventory;
using WpfDormitories.DataBase.Entity.Resident;
using WpfDormitories.DataBase.Entity.Room;
using WpfDormitories.DataBase.Entity.Street;
using WpfDormitories.DataBase;
using WpfDormitories.TemporarySolutions;

namespace WpfDormitories.Model.Services.Tables
{
    public class ContractsTableService : ITableService
    {
        private List<IContractData> _contracts;
        public Action<DataRow> OnEdit { get; set; }
        public Action OnAdd { get; set; }

        public void Edit(int index)
        {
            OnEdit.Invoke(DataTableParser.ToDataTable<IContractData>(_contracts).Rows[index]);
        }
        public DataTable FindAll(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return Read();
            }

            _contracts = DataManager.GetInstance().ContractsRepository.Read().ToList();

            DataTable res = CreateDataTable();

            List<IContractData> resContracts = new();

            foreach (IContractData contract in _contracts)
            {
                if (contract.Name.ToUpper().Contains(text.ToUpper()) || contract.DocumentNumber.Contains(text) || contract.WhoGave.ToUpper().Contains(text.ToUpper()))
                {
                    resContracts.Add(contract);
                }
            }
            _contracts = resContracts;

            foreach (IContractData contract in _contracts)
            {
                res.Rows.Add(contract.DocumentNumber, contract.Name, contract.WhoGave, contract.StartAction.ToString("yyyy-MM-dd"));
            }
            return res;
        }

        virtual public DataTable Read()
        {
            _contracts = DataManager.GetInstance().ContractsRepository.Read().ToList();
            DataTable res = CreateDataTable();
            foreach (IContractData contract in _contracts)
            {
                res.Rows.Add(contract.DocumentNumber, contract.Name, contract.WhoGave, contract.StartAction.ToString("yyyy-MM-dd"));
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
            return res;
        }

        public void Add()
        {
            OnAdd.Invoke();
        }

        public void Delete(int index)
        {
            List<IResidentData> residents = DataManager.GetInstance().ResidentsRepository.Read().ToList();
            List<IChildData> children = DataManager.GetInstance().ChildrenRepository.Read().ToList();
            List<IParentsAndChildrenData> parentsAndChildren = DataManager.GetInstance().ParentsAndChildrenRepository.Read().ToList();

            List<IResidentData> residentsInContract = residents.FindAll(item => item.ContractId == _contracts[index].Id);


            foreach (IResidentData resident in residentsInContract)
            {
                List<IParentsAndChildrenData> parentsAndChildrenByParent = parentsAndChildren.FindAll(item => item.ParentId == resident.Id);
                foreach (IParentsAndChildrenData parentAndChild in parentsAndChildrenByParent)
                {
                    DataManager.GetInstance().ParentsAndChildrenRepository.Delete(parentAndChild);
                }

            }
            foreach (IResidentData resident in residentsInContract)
            {
                DataManager.GetInstance().ResidentsRepository.Delete(resident);
            }

            parentsAndChildren = DataManager.GetInstance().ParentsAndChildrenRepository.Read().ToList();
            foreach (IChildData child in children)
            {
                List<IParentsAndChildrenData> parentsAndChildrenByChildren = parentsAndChildren.FindAll(item => item.ChildId == child.Id);
                if (parentsAndChildrenByChildren.Count == 0)
                {
                    DataManager.GetInstance().ChildrenRepository.Delete(child);
                }
            }

            DataManager.GetInstance().ContractsRepository.Delete(_contracts[index]);
            _contracts.Remove(_contracts[index]);
        }
        public DataRow GetByIndex(int index)
        {
            return DataTableParser.ToDataTable<IContractData>(_contracts).Rows[index];
        }
    }
}
