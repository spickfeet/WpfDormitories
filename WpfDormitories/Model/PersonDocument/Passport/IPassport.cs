using WpfDormitories.Model.FullName;

namespace WpfDormitories.Model.PersonDocument.Passport
{
    public interface IPassport
    {
        IFullName FullName { get; set; }
        string Gender { get; set; }
        DateOnly DateOfBirth { get; set; }
        int Series {  get; set; }
        int Number {  get; set; }
        DateOnly DateOfIssue { get; set; }
        string WhoGave { get; set; }
    }
}
