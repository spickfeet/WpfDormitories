using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfDormitories.Model.Convertors
{
    public interface IConvertor<R,V> where R: notnull where V : notnull
    {
        public R Convert(V value);
    }
}
