using WpfDormitories.Model.FullName;

namespace WpfDormitories.DataBase.Entity.Child
{
    public interface IChildData
    {
        uint Id { get; set; }
        string Gender { get; set; }
        DateOnly DateOfBirth { get; set; }
        IFullName FullName { get; set; }

    }
}
