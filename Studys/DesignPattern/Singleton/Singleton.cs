using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singleton
{
    public sealed class Singleton
    {
        public static Singleton Instance
        {
            get { return singleton; }
        }

        private static readonly Singleton singleton = new Singleton();

        private Singleton()
        {
            Console.WriteLine("インスタンスを生成しました。");
        }
    }
}
