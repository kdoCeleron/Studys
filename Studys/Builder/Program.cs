using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Builder.Interface;

namespace Builder
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Usage();
                return;
            }
            if (args[0].Equals("plain"))
            {
                TextBuilder textbuilder = new TextBuilder();
                string result = DoWork(textbuilder);
                Console.WriteLine(result);
            }
            else if (args[0].Equals("html"))
            {
                HTMLBuilder htmlbuilder = new HTMLBuilder();
                string filename = DoWork(htmlbuilder);
                Console.WriteLine(filename + "が作成されました。");
            }
            else
            {
                Usage();
                return;
            }
        }

        private static string DoWork(IBuilder builder)
        {
            Director director = new Director(builder);
            director.Construct();
            return builder.Result;
        }

        private static void Usage()
        {
            Console.WriteLine("Usage: BuilderSample plain プレーンテキストで文書作成");
            Console.WriteLine("Usage: BuilderSample html  HTMLファイルで文書作成");
        }
    }
}
