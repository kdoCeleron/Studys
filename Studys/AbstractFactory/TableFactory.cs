using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractFactory
{
    public class TableFactory : Factory
    {
        public override Link CreateLink(string caption, string url)
        {
            return new TableLink(caption, url);
        }

        public override Tray CreateTray(string caption)
        {
            return new TableTray(caption);
        }

        public override Page CreatePage(string title, string author)
        {
            return new TablePage(title, author);
        }
    }

    public class TableLink : Link
    {
        public TableLink(string caption, string url)
            : base(caption, url)
        {
        }

        public override string MakeHTML()
        {
            return "<td><a href=\"" + url + "\">" + caption + "</a></td>\n";
        }
    }

    public class TablePage : Page
    {
        public TablePage(string title, string author)
            : base(title, author)
        {
        }

        public override string MakeHTML()
        {
            StringBuilder buffer = new StringBuilder();
            buffer.Append("<html><head><title>" + title + "</title></head>\n");
            buffer.Append("<body>\n");
            buffer.Append("<h1>" + title + "</h1>\n");
            buffer.Append("<table width=\"80%\" border=\"3\">\n");

            content.ForEach(item =>
                buffer.Append("<tr>" + item.MakeHTML() + "</tr>"));

            buffer.Append("</table>\n");
            buffer.Append("<hr><address>" + author + "</address>");
            buffer.Append("</body></html>\n");
            return buffer.ToString();
        }
    }

    public class TableTray : Tray
    {
        public TableTray(string caption)
            : base(caption)
        {
        }

        public override string MakeHTML()
        {
            StringBuilder buffer = new StringBuilder();
            buffer.Append("<td>");
            buffer.Append("<table width=\"100%\" border=\"1\"><tr>");
            buffer.Append("<td bgcolor=\"#cccccc\" align=\"center\" colspan=\"" +
                          tray.Count + "\"><b>" + caption + "</b></td>");
            buffer.Append("</tr>\n");
            buffer.Append("<tr>\n");

            tray.ForEach(item =>
                buffer.Append(item.MakeHTML()));

            buffer.Append("</tr></table>");
            buffer.Append("</td>");
            return buffer.ToString();
        }
    }
}
