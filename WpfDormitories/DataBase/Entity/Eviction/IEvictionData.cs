using System.ComponentModel;
using WpfDormitories.Model.PersonDocument;

namespace WpfDormitories.DataBase.Entity.Eviction
{
    public interface IEvictionData
    {
        uint Id { get; set; }
        IPersonDocuments PersonDocuments { get; set; }
        [DisplayName("Причина выселения")]
        string Reason {  get; set; }
        [DisplayName("Дата выселения")]
        DateOnly Date { get; set; }
    }
}
