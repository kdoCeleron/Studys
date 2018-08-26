using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractFactory
{
    public abstract class Factory
    {

        public static Factory GetFactory(string classname)
        {
            Factory factory = null;
            try
            {
                factory = (Factory)Activator.CreateInstance(Type.GetType(classname));
            }
            catch (System.TypeLoadException)
            {
                Console.Error.WriteLine("クラス " + classname + " が見つかりません。");
            }
            catch (System.Exception e)
            {
                Console.Error.WriteLine(e.StackTrace);
            }
            return factory;
        }

        public abstract Link CreateLink(string caption, string url);
        public abstract Tray CreateTray(string caption);
        public abstract Page CreatePage(string title, string author);
    }

    public abstract class Item
    {
        protected string caption;

        public Item(string caption)
        {
            this.caption = caption;
        }

        public abstract string MakeHTML();
    }

    public abstract class Link : Item
    {
        protected string url;

        public Link(string caption, string url)
            : base(caption)
        {
            this.url = url;
        }
    }

    public abstract class Page
    {
        protected string title;
        protected string author;
        protected List<Item> content = new List<Item>();

        public Page(string title, string author)
        {
            this.title = title;
            this.author = author;
        }

        public void Add(Item item)
        {
            this.content.Add(item);
        }

        public void Output()
        {
            try
            {
                string filename = this.title + ".html";
                StreamWriter writer = new StreamWriter(filename, false, Encoding.Default);
                writer.Write(this.MakeHTML());
                writer.Close();
                Console.WriteLine(filename + " を作成しました。");
            }
            catch (System.IO.IOException e)
            {
                Console.Error.WriteLine(e.StackTrace);
            }
        }

        public abstract string MakeHTML();
    }

    public abstract class Tray : Item
    {
        protected List<Item> tray = new List<Item>();

        public Tray(string caption)
            : base(caption)
        {
        }

        public void Add(Item item)
        {
            tray.Add(item);
        }
    }
}
