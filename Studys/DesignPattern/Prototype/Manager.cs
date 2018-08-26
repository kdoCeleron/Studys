using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prototype.Interface;

namespace Prototype
{
    public class Manager
    {
        private Dictionary<string, IProduct> showcase = new Dictionary<string, IProduct>();
        public virtual void Register(string name, IProduct proto)
        {
            showcase[name] = proto;
        }

        // 複製を返すのが肝。Flyweightとはここが異なる。
        public virtual IProduct Create(string protoname)
        {
            IProduct p = showcase[protoname];
            return p.Clone();
        }
    }
}
