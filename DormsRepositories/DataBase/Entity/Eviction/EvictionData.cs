using WpfDormitories.Model.PersonDocument;

namespace WpfDormitories.DataBase.Entity.Eviction
{
    public class EvictionData : IEvictionData
    {
        public uint Id { get; set; }
        public IPersonDocuments PersonDocuments { get; set; }
        public string Reason { get; set; }
        public DateTime Date { get; set; }

        public EvictionData(uint id, IPersonDocuments personDocuments, string reason, DateTime date)
        {
            Id = id;
            PersonDocuments = personDocuments;
            Reason = reason;
            Date = date;
        }

        public EvictionData(IPersonDocuments personDocuments, string reason, DateTime date)
        {
            PersonDocuments = personDocuments;
            Reason = reason;
            Date = date;
        }
    }
}
