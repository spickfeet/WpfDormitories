using System.ComponentModel;
using WpfDormitories.Model.PersonDocument.Passport;

namespace WpfDormitories.Model.PersonDocument
{
    public interface IPersonDocuments
    {
        [DisplayName("Регистрационный номер")]
        string RegistrationNumber { get; set; }
        IPassport Passport { get; set; }
    }
}
