namespace WpfDormitories.DataBase.Entity.MenuElement
{
    public interface IMenuElementData
    {
        uint Id { get; set; }
        uint ParentId { get; set; }
        string Name { get; set; }
        string DllName { get; set; }
        string FuncName { get; set; }
        uint Order { get; set; }
    }
}
