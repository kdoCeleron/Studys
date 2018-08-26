using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prototype.Interface;

namespace Prototype
{
    public class UnderlinePen : IProduct
    {
        private char ulchar;
        public UnderlinePen(char ulchar)
        {
            this.ulchar = ulchar;
        }

        public virtual void Use(string s)
        {
            int length = Encoding.GetEncoding("shift-jis").GetByteCount(s);

            Console.WriteLine("\"{0}\"", s);

            Console.Write(" ");
            Console.WriteLine(new string(ulchar, length));
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
