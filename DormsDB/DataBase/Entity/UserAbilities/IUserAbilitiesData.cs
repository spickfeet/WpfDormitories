using System.ComponentModel;

namespace WpfDormitories.DataBase.Entity.UserAbilities
{
    public interface IUserAbilitiesData
    {
        uint Id { get; set; }
        uint UserId { get; set; }
        uint MenuElementId { get; set; }
        [DisplayName("Просмотр")]
        bool R {  get; set; }
        [DisplayName("Запись")]
        bool W { get; set; }
        [DisplayName("Редактирование")]
        bool E { get; set; }
        [DisplayName("Удаление")]
        bool D { get; set; }
    }
}
