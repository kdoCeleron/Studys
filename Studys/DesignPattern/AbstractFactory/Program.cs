using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractFactory
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                const string ListFactoryClass =
                    "Gushwell.DesignPatterns.List.ListFactory";
                const string TableFactoryClass =
                    "Gushwell.DesignPatterns.Table.TableFactory";
                Console.WriteLine("Usage: AbstractFactory TypeName");
                Console.WriteLine("Example 1: AbstractFactory " + ListFactoryClass);
                Console.WriteLine("Example 2: AbstractFactory " + TableFactoryClass);
                return;
            }
            Factory factory = Factory.GetFactory(args[0]);

            Link asahi = factory.CreateLink("朝日新聞", "http://www.asahi.com/");
            Link yomiuri = factory.CreateLink("読売新聞", "http://www.yomiuri.co.jp/");

            Link us_yahoo = factory.CreateLink("Yahoo!", "http://www.yahoo.com/");
            Link jp_yahoo = factory.CreateLink("Yahoo!Japan", "http://www.yahoo.co.jp/");
            Link excite = factory.CreateLink("Excite", "http://www.excite.com/");
            Link google = factory.CreateLink("Google", "http://www.google.com/");

            Tray traynews = factory.CreateTray("新聞");
            traynews.Add(asahi);
            traynews.Add(yomiuri);

            Tray trayyahoo = factory.CreateTray("Yahoo!");
            trayyahoo.Add(us_yahoo);
            trayyahoo.Add(jp_yahoo);

            Tray traysearch = factory.CreateTray("サーチエンジン");
            traysearch.Add(trayyahoo);
            traysearch.Add(excite);
            traysearch.Add(google);

            Page page = factory.CreatePage("LinkPage", "結城 浩");
            page.Add(traynews);
            page.Add(traysearch);
            page.Output();
        }
    }
}
