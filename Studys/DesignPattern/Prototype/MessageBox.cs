using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prototype.Interface;

namespace Prototype
{
    public class MessageBox : IProduct
    {
        private char decoChar;
        public MessageBox(char decochar)
        {
            this.decoChar = decochar;
        }

        public virtual void Use(string s)
        {
            int length = Encoding.GetEncoding("shift-jis").GetByteCount(s) + 4;
            Console.WriteLine(new string(decoChar, length));

            Console.WriteLine("{0} {1} {0}", decoChar, s);

            Console.WriteLine(new string(decoChar, length));
        }

        public IProduct Clone()
        {
            return (IProduct)MemberwiseClone();
        }
        object ICloneable.Clone()
        {
            return this.Clone();
        }
    }
}
