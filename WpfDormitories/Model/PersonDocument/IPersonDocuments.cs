using WpfDormitories.Model.PersonDocument.Passport;

namespace WpfDormitories.Model.PersonDocument
{
    public interface IPersonDocuments
    {
        string RegistrationNumber { get; set; }
        IPassport Passport { get; set; }
    }
}
