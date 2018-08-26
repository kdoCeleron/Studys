using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Builder.Interface;
using Studys.Common.Extensions;

namespace Builder
{
    public class TextBuilder : IBuilder
    {
        // このフィールドに文書を構築していく
        private StringBuilder buffer = new StringBuilder();

        // 完成した文書
        public string Result
        {
            get { return buffer.ToString(); }
        }

        // プレーンテキストでのタイトル
        public void MakeTitle(string title)
        {
            buffer.AppendLine("=============================="); // 飾り線
            buffer.AppendLine("『" + title + "』");              // 『』つきのタイトル
            buffer.AppendLine();                                 // 空行
        }

        // プレーンテキストでの文字列
        public void MakeString(string str)
        {
            buffer.AppendLine('■' + str);                  // ■つきの文字列
            buffer.AppendLine();                            // 空行
        }

        // プレーンテキストでの箇条書き
        public void MakeItems(string[] items)
        {
            items.ForEach(str =>
                buffer.AppendLine("　・" + str));
            buffer.AppendLine();                            // 空行
        }

        // 文書の完成
        public void Close()
        {
            buffer.AppendLine("=============================="); // 飾り線
        }
    }
}
