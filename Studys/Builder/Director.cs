using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Builder.Interface;

namespace Builder
{
    public class Director
    {
        private IBuilder builder;

        public Director(IBuilder builder)
        {
            // Builderのサブクラスのインスタンスが与えられるので、
            // builderフィールドに保持しておく。
            this.builder = builder;
        }

        public void Construct()
        {
            // builderを使って文書構築
            builder.MakeTitle("Greeting");              // タイトル
            builder.MakeString("朝から昼にかけて");     // 文字列
            builder.MakeItems(new string[] { "おはようございます。", "こんにちは。" });
            builder.MakeString("夜に");                 // 別の文字列
            builder.MakeItems(new string[] { "こんばんは。", "おやすみなさい。", "さようなら。" });
            builder.Close();                            // 文書を完成させる
        }
    }
}
