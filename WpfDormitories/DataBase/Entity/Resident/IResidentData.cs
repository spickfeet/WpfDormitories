using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        [DisplayName("Наличие детей")]
        bool HaveChildren { get; set; }
        [DisplayName("Дата заселения")]
        DateOnly ArrivalDate { get; set; }
        [DisplayName("Оплата")]
        float Payment {  get; set; }
        [DisplayName("Место работы")]
        string PlaceOfWork { get; set; }
        [DisplayName("Место учебы")]
        string PlaceOfStudy { get; set; }
    }
}
