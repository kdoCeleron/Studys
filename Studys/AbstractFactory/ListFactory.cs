using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractFactory
{
    public class ListFactory : Factory
    {
        public override Link CreateLink(string caption, string url)
        {
            return new ListLink(caption, url);
        }

        public override Tray CreateTray(string caption)
        {
            return new ListTray(caption);
        }

        public override Page CreatePage(string title, string author)
        {
            return new ListPage(title, author);
        }
    }

    public class ListLink : Link
    {
        public ListLink(string caption, string url)
            : base(caption, url)
        {
        }

        public override string MakeHTML()
        {
            return "  <li><a href=\"" + url + "\">" + caption + "</a></li>\n";
        }
    }

    public class ListPage : Page
    {
        public ListPage(string title, string author)
            : base(title, author)
        {
        }

        public override string MakeHTML()
        {
            StringBuilder buffer = new StringBuilder();
            buffer.Append("<html><head><title>" + title + "</title></head>\n");
            buffer.Append("<body>\n");
            buffer.Append("<h1>" + title + "</h1>\n");
            buffer.Append("<ul>\n");
            content.ForEach(item =>
                buffer.Append(item.MakeHTML()));
            buffer.Append("</ul>\n");
            buffer.Append("<hr><address>" + author + "</address>");
            buffer.Append("</body></html>\n");
            return buffer.ToString();
        }
    }

    public class ListTray : Tray
    {
        public ListTray(string caption)
            : base(caption)
        {
        }

        public override string MakeHTML()
        {
            StringBuilder buffer = new StringBuilder();
            buffer.Append("<li>\n");
            buffer.Append(caption + "\n");
            buffer.Append("<ul>\n");
            tray.ForEach(item =>
                buffer.Append(item.MakeHTML()));
            buffer.Append("</ul>\n");
            buffer.Append("</li>\n");
            return buffer.ToString();
        }
    }
}
