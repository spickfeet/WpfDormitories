using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfDormitories.Model.PersonDocument;

namespace WpfDormitories.DataBase.Entity.Resident
{
    public class ResidentData : IResidentData
    {
        public uint Id { get; set; }
        public uint ContractId { get; set; }
        public uint RoomId { get; set; }
        public IPersonDocuments PersonDocuments { get; set; }
        public bool HaveChildren { get; set; }
        public DateTime ArrivalDate { get; set; }
        public float Payment { get; set; }
        public string PlaceOfWork { get; set; }
        public string PlaceOfStudy { get; set; }

        public ResidentData(uint id, uint contractId, uint roomId, IPersonDocuments personDocuments,
            bool haveChildren, DateTime arrivalDate, float payment, string placeOfWork, string placeOfStudy) 
        { 
            Id = id;
            ContractId = contractId;
            RoomId = roomId;
            PersonDocuments = personDocuments;
            HaveChildren = haveChildren;
            ArrivalDate = arrivalDate;
            Payment = payment;
            PlaceOfWork = placeOfWork;
            PlaceOfStudy = placeOfStudy;
        }

        public ResidentData(uint contractId, uint roomId, IPersonDocuments personDocuments,
    bool haveChildren, DateTime arrivalDate, float payment, string placeOfWork, string placeOfStudy)
        {
            ContractId = contractId;
            RoomId = roomId;
            PersonDocuments = personDocuments;
            HaveChildren = haveChildren;
            ArrivalDate = arrivalDate;
            Payment = payment;
            PlaceOfWork = placeOfWork;
            PlaceOfStudy = placeOfStudy;
        }
    }
}
