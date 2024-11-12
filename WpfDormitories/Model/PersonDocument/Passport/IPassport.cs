using WpfDormitories.Model.FullName;

namespace WpfDormitories.Model.PersonDocument.Passport
{
    public interface IPassport
    {
        IFullName FullName { get; set; }
        string Gender { get; set; }
        DateOnly DateOfBirth { get; set; }
        uint Series {  get; set; }
        uint Number {  get; set; }
        DateOnly DateOfIssue { get; set; }
        string WhoGave { get; set; }
    }
}
