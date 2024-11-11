namespace WpfDormitories.DataBase.Entity.Inventory
{
    public interface IInventoryData
    {
        uint Id { get; set; }
        uint RoomId { get; set; }
        uint NameId { get; set; }
    }
}
