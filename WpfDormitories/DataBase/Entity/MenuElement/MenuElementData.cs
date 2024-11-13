namespace WpfDormitories.DataBase.Entity.MenuElement
{
    public class MenuElementData : IMenuElementData
    {
        public uint Id { get; set; }
        public uint ParentId { get; set; }
        public string Name { get; set; }
        public string DllName { get; set; }
        public string FuncName { get; set; }
        public uint Order { get; set; }

        public MenuElementData(uint id, uint parentId, string name, string dllName, string funcName, uint order) 
        {
            Id = id;
            ParentId = parentId;
            Name = name;
            DllName = dllName;
            FuncName = funcName;
            Order = order;
        }
    }
}
