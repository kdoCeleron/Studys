using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Builder.Interface;
using Studys.Common.Extensions;

namespace Builder
{

    public class HTMLBuilder : IBuilder
    {
        private string filename;        // 作成するファイル名
        private StreamWriter writer;    // ファイルに書き込む

        // 完成した文書
        public string Result
        {
            get { return filename; }    // ファイル名を返す
        }

        // HTMLファイルでのタイトル
        public void MakeTitle(string title)
        {
            filename = title + ".html"; // タイトルを元にファイル名決定
            try
            {
                writer = new StreamWriter(filename, false);
            }
            catch (System.IO.IOException e)
            {
                Console.WriteLine(e.StackTrace);
            }
            // タイトルを出力
            writer.WriteLine("<html><head><title>" + title + "</title></head><body>");
            writer.WriteLine("<meta http-equiv=\"Content-Type\" " +
                             "content=\"text/html; charset=UTF-8\" />");
            writer.WriteLine("<h1>" + title + "</h1>");
        }

        // HTMLファイルでの文字列
        public void MakeString(string str)
        {
            writer.WriteLine("<p>" + str + "</p>");         // <p>タグで出力
        }

        // HTMLファイルでの箇条書き
        public void MakeItems(string[] items)
        {
            writer.WriteLine("<ul>");                       // <ul>と<li>で出力
            items.ForEach(str => writer.WriteLine("<li>" + str + "</li>"));
            writer.WriteLine("</ul>");
        }

        // 文書の完成
        public void Close()
        {
            writer.WriteLine("</body></html>");             // タグを閉じる
            writer.Close();                                 // ファイルをクローズ
        }
    }

    // なぜ、HTMLBuilderは、ファイルに出力するのに、TextBuilderはファイルに出力しない設計
    // なのか不明。僕ならば、両方stringとして結果を得られるようにし、ファイルに出力するか
    // しないかは、利用する側で決められるようにする。

}
