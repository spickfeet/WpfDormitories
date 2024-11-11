using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfDormitories.Model.PersonDocument;

namespace WpfDormitories.DataBase.Entity.Resident
{
    public interface IResidentData
    {
        uint Id { get; set; }
        uint ContractId { get; set; }
        uint RoomId { get; set; }
        IPersonDocuments PersonDocuments { get; set; }
        bool HaveChildren { get; set; }
        DateOnly ArrivalDate { get; set; }
        float Payment {  get; set; }
        string PlaceOfWork { get; set; }
        string PlaceOfStudy { get; set; }
    }
}
