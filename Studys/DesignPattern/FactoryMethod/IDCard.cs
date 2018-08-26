using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FactoryMethod.Interface;

namespace FactoryMethod
{
    public class IDCard : IProduct
    {
        private string owner;

        public IDCard(string owner)
        {
            Console.WriteLine("{0} のカードを作ります。", owner);
            this.owner = owner;
        }

        public void Use()
        {
            Console.WriteLine("{0} のカードを使います。", owner);
        }

        public string Owner
        {
            get { return owner; }
        }
    }
}
