using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfDormitories.DataBase.Entity.Contract
{
    public class ContractData : IContractData
    {
        public uint Id { get; set; }
        public uint DocumentNumber { get; set; }
        public string Name { get; set; }
        public string WhoGave { get; set; }
        public DateOnly StartAction { get; set; }
        public string Comment { get; set; }

        public ContractData(uint id, uint documentNumber, string name, string whoGave, DateOnly startAction, string comment) 
        {
            Id = id;
            DocumentNumber = documentNumber;
            Name = name;
            WhoGave = whoGave;
            StartAction = startAction;
            Comment = comment;
        }
        public ContractData(uint documentNumber, string name, string whoGave, DateOnly startAction, string comment)
        {
            DocumentNumber = documentNumber;
            Name = name;
            WhoGave = whoGave;
            StartAction = startAction;
            Comment = comment;
        }
    }
}
