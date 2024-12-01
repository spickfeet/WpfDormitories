using System.ComponentModel;
using WpfDormitories.Model.FullName;

namespace WpfDormitories.Model.PersonDocument.Passport
{
    public interface IPassport
    {
        IFullName FullName { get; set; }
        [DisplayName("Пол")]
        string Gender { get; set; }
        [DisplayName("Дата рождения")]
        DateTime DateOfBirth { get; set; }
        [DisplayName("Серия паспорта")]
        uint Series {  get; set; }
        [DisplayName("Номер паспорта")]
        uint Number {  get; set; }
        [DisplayName("Дата выдачи паспорта")]
        DateTime DateOfIssue { get; set; }
        [DisplayName("Кто выдал паспорт")]
        string WhoGave { get; set; }
    }
}
