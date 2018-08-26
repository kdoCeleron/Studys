using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Singleton
{
    public class Program
    {
        public static void Main(System.String[] args)
        {
            Console.WriteLine("Start.");
            Singleton obj1 = Singleton.Instance;
            Singleton obj2 = Singleton.Instance;
            if (obj1 == obj2)
            {
                Console.WriteLine("obj1とobj2は同じインスタンスです。");
            }
            else
            {
                Console.WriteLine("obj1とobj2は同じインスタンスではありません。");
            }

            Console.WriteLine("End.");

            Triple t0 = Triple.GetInstance(0);
            Triple t1 = Triple.GetInstance(1);
            Triple t2 = Triple.GetInstance(2);
            Triple t3 = Triple.GetInstance(0);
            Triple t4 = Triple.GetInstance(1);
            Triple t5 = Triple.GetInstance(2);
            if (t0 == t1)
            {
                Console.WriteLine("t0とt1は同じインスタンスです。");
            }

            if (t0 == t2)
            {
                Console.WriteLine("t0とt2は同じインスタンスです。");
            }

            if (t0 == t3)
            {
                Console.WriteLine("t0とt3は同じインスタンスです。");
            }

            if (t0 == t4)
            {
                Console.WriteLine("t0とt4は同じインスタンスです。");
            }

            if (t0 == t5)
            {
                Console.WriteLine("t0とt5は同じインスタンスです。");
            }

            if (t1 == t2)
            {
                Console.WriteLine("t1とt2は同じインスタンスです。");
            }

            if (t1 == t3)
            {
                Console.WriteLine("t1とt3は同じインスタンスです。");
            }

            if (t1 == t4)
            {
                Console.WriteLine("t1とt4は同じインスタンスです。");
            }

            if (t1 == t5)
            {
                Console.WriteLine("t1とt5は同じインスタンスです。");
            }

            Thread.Sleep(10000);
        }
    }
}
