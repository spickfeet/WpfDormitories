using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfDormitories.DataBase.Entity.Child.ParentsAndChildren
{
    internal class ParentsAndChildrenData : IParentsAndChildrenData
    {
        public uint Id { get; set; }
        public uint ParentId { get; set; }
        public uint ChildId { get; set; }
        public ParentsAndChildrenData(uint id, uint parentId, uint childId) 
        {
            Id = id;
            ParentId = parentId;
            ChildId = childId;
        }
    }
}
