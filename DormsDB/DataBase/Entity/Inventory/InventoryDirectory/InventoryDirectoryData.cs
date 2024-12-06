namespace WpfDormitories.DataBase.Entity.Inventory.InventoryDirectory
{
    internal class InventoryDirectoryData : IInventoryDirectoryData
    {
        public uint Id { get; set; }
        public string Name { get; set; }

        public InventoryDirectoryData(uint id, string name) 
        {
            Id = id;
            Name = name;
        }

        public InventoryDirectoryData(string name)
        {
            Name = name;
        }
    }
}
