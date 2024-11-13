namespace WpfDormitories.DataBase.Entity.UserAbilities
{
    public interface IUserAbilitiesData
    {
        uint Id { get; set; }
        uint UserId { get; set; }
        uint MenuElementId { get; set; }
        bool R {  get; set; }
        bool W { get; set; }
        bool E { get; set; }
        bool D { get; set; }
    }
}
