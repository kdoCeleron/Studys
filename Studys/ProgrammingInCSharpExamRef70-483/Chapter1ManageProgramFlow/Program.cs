using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace Chapter1ManageProgramFlow
{
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// メインプログラム
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed. Suppression is OK here.")]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1309:FieldNamesMustNotBeginWithUnderscore", Justification = "Reviewed. Suppression is OK here.")]
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1400:AccessModifierMustBeDeclared", Justification = "Reviewed. Suppression is OK here.")]
    public class Program
    {
        /// <summary>
        /// 実行処理マップ
        /// </summary>
        private static Dictionary<int, Action> _actionMap = new Dictionary<int, Action>();

        /// <summary>
        /// メイン
        /// </summary>
        /// <param name="args">引数</param>
        static void Main(string[] args)
        {
            InitActionMap();

            char[] value;
            while (true)
            {
                value = new char[3];

                Console.WriteLine("InputExecNo");

                bool isInvalidValue = false;
                for (int index = 0; index < value.Length; index++)
                {
                    var key = Console.ReadKey();
                    if (key.Key == ConsoleKey.Enter)
                    {
                        break;
                    }

                    int tmp;
                    var canRead = int.TryParse(key.KeyChar.ToString(), out tmp);
                    if (!canRead)
                    {
                        isInvalidValue = true;
                        break;
                    }

                    value[index] = key.KeyChar;
                }

                if (isInvalidValue)
                {
                    break;
                }

                int no;
                var str = new string(value);
                var parse = int.TryParse(str, out no);
                if (!parse)
                {
                    break;
                }

                if (!_actionMap.ContainsKey(no))
                {
                    break;
                }

                Console.WriteLine("==========");
                Console.WriteLine("Execut:No[{0:D3}]", no);

                var action = _actionMap[no];
                Console.WriteLine("Start Function: [{0}]", action.Method.Name);
                _actionMap[no]();
                Console.WriteLine("End   Function: [{0}]", action.Method.Name);

                Console.ReadKey();
            }

            Console.WriteLine("Program End");
            Console.WriteLine("===========");
        }

        /// <summary>
        /// 実行処理マップを初期化します。
        /// </summary>
        private static void InitActionMap()
        {
            _actionMap.Add(1, Listen1Actions.No01ParallellInvoke);
            _actionMap.Add(2, Listen1Actions.No02ParallellForEach);
            _actionMap.Add(3, Listen1Actions.No03_ParallellFor);
            _actionMap.Add(4, Listen1Actions.No04ParallellForLoop);
            _actionMap.Add(5, Listen1Actions.No05ParallelLinqQuery);
            _actionMap.Add(6, Listen1Actions.No06InformingParallelization);
            _actionMap.Add(7, Listen1Actions.No07UsingAsOrdered);
            _actionMap.Add(8, Listen1Actions.No08SequentialElements);
            _actionMap.Add(9, Listen1Actions.No09UsingForAll);
            _actionMap.Add(10, Listen1Actions.No10ExceptionsInPLINQ);
            _actionMap.Add(11, Listen1Actions.No11CreateTask);
            _actionMap.Add(12, Listen1Actions.No12RunTask);
            _actionMap.Add(13, Listen1Actions.No13TaskFactory);
        }
    }
}