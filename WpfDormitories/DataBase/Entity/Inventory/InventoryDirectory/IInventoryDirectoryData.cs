using System.ComponentModel;

namespace WpfDormitories.DataBase.Entity.Inventory.InventoryDirectory
{
    public interface IInventoryDirectoryData
    {
        uint Id { get; set; }

        [DisplayName("Инвентарь")]
        string Name { get; set; }
    }
}
