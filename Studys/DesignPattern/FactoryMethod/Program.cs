using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FactoryMethod.Interface;
using Studys.Common.Extensions;

namespace FactoryMethod
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Factoryクラスは、IProductを実装していれば、IDCard以外のインスタンスも生成できる
            var factory = new Factory<IDCard>();

            // IProduct(インターフェース)経由でアクセス
            IProduct card1 = factory.Create("結城浩");
            IProduct card2 = factory.Create("とむら");
            IProduct card3 = factory.Create("佐藤花子");
            card1.Use();
            card2.Use();
            card3.Use();

            factory.Owners.ForEach(owner => Console.WriteLine(owner));
            
            Thread.Sleep(10000);
        }
    }
}
