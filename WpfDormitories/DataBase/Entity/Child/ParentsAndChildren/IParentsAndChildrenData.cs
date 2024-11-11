using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfDormitories.DataBase.Entity.Child.ParentsAndChildren
{
    public interface IParentsAndChildrenData
    {
        uint Id { get; set; }
        uint ParentId { get; set; }
        uint ChildId { get; set; }
    }
}
