using WpfDormitories.Model.PersonDocument;

namespace WpfDormitories.DataBase.Entity.Eviction
{
    public interface IEvictionData
    {
        uint Id { get; set; }
        IPersonDocuments PersonDocuments { get; set; }
        string Reason {  get; set; }
        DateOnly Date { get; set; }
    }
}
