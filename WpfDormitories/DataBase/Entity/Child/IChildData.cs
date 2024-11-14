using System.ComponentModel;
using WpfDormitories.Model.FullName;

namespace WpfDormitories.DataBase.Entity.Child
{
    public interface IChildData
    {
        uint Id { get; set; }
        [DisplayName("Пол")]
        string Gender { get; set; }
        [DisplayName("Дата рождения")]
        DateOnly DateOfBirth { get; set; }
        IFullName FullName { get; set; }

    }
}
