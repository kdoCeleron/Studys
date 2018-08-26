using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singleton
{
    public class Triple
    {
        private static int maxInstanceNum = 3;

        public static Triple GetInstance(int n)
        {
            if (n >= maxInstanceNum)
            {
                return null;
            }

            return triple[n];
        }

        private static readonly Triple[] triple =
            new[] { new Triple(), new Triple(), new Triple() };

        private Triple()
        {
            Console.WriteLine("インスタンスを生成しました。");
        }
    }
}
