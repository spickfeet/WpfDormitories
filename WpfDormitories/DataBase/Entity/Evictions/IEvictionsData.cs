using WpfDormitories.Model.PersonDocument;

namespace WpfDormitories.DataBase.Entity.Evictions
{
    public interface IEvictionsData
    {
        uint Id { get; set; }
        IPersonDocuments PersonDocuments { get; set; }
        string Reason {  get; set; }
        DateOnly Date { get; set; }
    }
}
