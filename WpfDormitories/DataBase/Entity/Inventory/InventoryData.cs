namespace WpfDormitories.DataBase.Entity.Inventory
{
    public class InventoryData : IInventoryData
    {
        public uint Id { get; set; }
        public uint RoomId { get; set; }
        public uint NameId { get; set; }

        public InventoryData(uint id, uint roomId, uint nameId)
        {
            Id = id;
            RoomId = roomId;
            NameId = nameId;
        }
    }
}