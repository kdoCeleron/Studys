using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace Chapter1ManageProgramFlow
{
    public class Program
    {
        private static Dictionary<int, Action> _actionMap = new Dictionary<int, Action>();

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

        public static void InitActionMap()
        {
            _actionMap.Add(1, Listen1_01_ParallellInvoke);
            _actionMap.Add(2, Listen1_02_ParallellForEach);
            _actionMap.Add(3, Listen1_03_ParallellFor);
            _actionMap.Add(4, Listen1_04_ParallellForLoop);
            _actionMap.Add(5, Listen1_05_ParallelLinqQuery);
            _actionMap.Add(6, Listen1_06_InformingParallelization);
            _actionMap.Add(7, Listen1_07_Using_AsOrdered);
            _actionMap.Add(8, Listen1_08_SequentialElements);
            _actionMap.Add(9, Listen1_09_UsingForAll);
            _actionMap.Add(10, Listen1_10_ExceptionsInPLINQ);
            _actionMap.Add(11, Listen1_11_CreateTask);
            _actionMap.Add(12, Listen1_12_RunTask);
            _actionMap.Add(13, Listen1_13_TaskFactory);
        }

        public static void Listen1_01_ParallellInvoke()
        {
            var task1 = new Action(() =>
            {
                Console.WriteLine("Task1 start");
                Thread.Sleep(2000);
                Console.WriteLine("Task1 end");
            });

            var task2 = new Action(() =>
            {
                Console.WriteLine("Task2 start");
                Thread.Sleep(2000);
                Console.WriteLine("Task2 end");
            });

            Parallel.Invoke(() => task1(), () => task2());
        }

        public static void Listen1_02_ParallellForEach()
        {
            var act = new Action<object>((obj) =>
            {
                Console.WriteLine("Started work :" + obj);
                Thread.Sleep(100);
                Console.WriteLine("Ended work :" + obj);
            });

            var items = Enumerable.Range(0, 500);
            Parallel.ForEach(items, item => { act(item); });
        }

        public static void Listen1_03_ParallellFor()
        {
            var act = new Action<object>((obj) =>
            {
                Console.WriteLine("Started work :" + obj);
                Thread.Sleep(100);
                Console.WriteLine("Ended work :" + obj);
            });

            var items = Enumerable.Range(0, 500).ToArray();
            Parallel.For(0, items.Length, i => act(items[i]));
        }

        public static void Listen1_04_ParallellForLoop()
        {
            var act = new Action<object>((obj) =>
            {
                Console.WriteLine("Started work :" + obj);
                Thread.Sleep(100);
                Console.WriteLine("Ended work   :" + obj);
            });

            var items = Enumerable.Range(0, 10).ToArray();

            ParallelLoopResult result = Parallel.For(0, items.Length, (int i, ParallelLoopState state) =>
            {
                Console.WriteLine("Loop Item :" + items[i]);

                if (i == 3)
                {
                    // index :4 以降のループ処理を実施しない。
                    state.Stop();
                }

                act(items[i]);
            });

            Console.WriteLine("Completed: " + result.IsCompleted);
            Console.WriteLine("Items: " + result.LowestBreakIteration);
        }

        public static void Listen1_05_ParallelLinqQuery()
        {
            var lst = new[]
            {
                new {No = 1, Name = "Alan", City = "Hull"},
                new {No = 2, Name = "Beryl", City = "Seattle"},
                new {No = 3, Name = "Charles", City = "London"},
                new {No = 4, Name = "David", City = "Seattle"},
                new {No = 5, Name = "Eddy", City = "Paris"},
                new {No = 6, Name = "Fred", City = "Berlin"},
                new {No = 7, Name = "Gordon", City = "Hull"},
                new {No = 8, Name = "Henry", City = "Seattle"},
                new {No = 9, Name = "Isaac", City = "Seattle"},
                new {No = 10, Name = "James", City = "London"}
            };

            var result = from item in lst.AsParallel()
                         where item.City == "Seattle"
                         select item;

            foreach (var item in result)
            {
                Console.WriteLine("No:{0}, Name:{1}", item.No, item.Name);
            }
        }

        public static void Listen1_06_InformingParallelization()
        {
            var lst = new[]
            {
                new {No = 1, Name = "Alan", City = "Hull"},
                new {No = 2, Name = "Beryl", City = "Seattle"},
                new {No = 3, Name = "Charles", City = "London"},
                new {No = 4, Name = "David", City = "Seattle"},
                new {No = 5, Name = "Eddy", City = "Paris"},
                new {No = 6, Name = "Fred", City = "Berlin"},
                new {No = 7, Name = "Gordon", City = "Hull"},
                new {No = 8, Name = "Henry", City = "Seattle"},
                new {No = 9, Name = "Isaac", City = "Seattle"},
                new {No = 10, Name = "James", City = "London"}
            };

            var result = from item in lst.AsParallel()
                    .WithDegreeOfParallelism(4)
                    .WithExecutionMode(ParallelExecutionMode.ForceParallelism) // クエリの並列
                         where item.City == "Seattle"
                         select item;

            foreach (var item in result)
            {
                Console.WriteLine("No:{0}, Name:{1}", item.No, item.Name);
            }
        }

        public static void Listen1_07_Using_AsOrdered()
        {
            var lst = new[]
            {
                new {No = 1, Name = "Alan", City = "Hull"},
                new {No = 2, Name = "Beryl", City = "Seattle"},
                new {No = 3, Name = "Charles", City = "London"},
                new {No = 4, Name = "David", City = "Seattle"},
                new {No = 5, Name = "Eddy", City = "Paris"},
                new {No = 6, Name = "Fred", City = "Berlin"},
                new {No = 7, Name = "Gordon", City = "Hull"},
                new {No = 8, Name = "Henry", City = "Seattle"},
                new {No = 9, Name = "Isaac", City = "Seattle"},
                new {No = 10, Name = "James", City = "London"}
            };

            var result = from item in lst.AsParallel().AsOrdered()
                where item.City == "Seattle"
                select item;

            foreach (var item in result)
            {
                Console.WriteLine("No:{0}, Name:{1}", item.No, item.Name);
            }
        }

        public static void Listen1_08_SequentialElements()
        {
            var lst = new[]
            {
                new {No = 1, Name = "Alan", City = "Hull"},
                new {No = 2, Name = "Beryl", City = "Seattle"},
                new {No = 3, Name = "Charles", City = "London"},
                new {No = 4, Name = "David", City = "Seattle"},
                new {No = 5, Name = "Eddy", City = "Paris"},
                new {No = 6, Name = "Fred", City = "Berlin"},
                new {No = 7, Name = "Gordon", City = "Hull"},
                new {No = 8, Name = "Henry", City = "Seattle"},
                new {No = 9, Name = "Isaac", City = "Seattle"},
                new {No = 10, Name = "James", City = "London"}
            };

            var result = (from item in lst.AsParallel()
                where item.City == "Seattle"
                orderby (item.Name)
                select new
                {
                    No = item.No,
                    Name = item.Name
                }).AsSequential().Take(3);

            foreach (var item in result)
            {
                Console.WriteLine("No:{0}, Name:{1}", item.No, item.Name);
            }
        }

        public static void Listen1_09_UsingForAll()
        {
            var lst = new[]
            {
                new {No = 1, Name = "Alan", City = "Hull"},
                new {No = 2, Name = "Beryl", City = "Seattle"},
                new {No = 3, Name = "Charles", City = "London"},
                new {No = 4, Name = "David", City = "Seattle"},
                new {No = 5, Name = "Eddy", City = "Paris"},
                new {No = 6, Name = "Fred", City = "Berlin"},
                new {No = 7, Name = "Gordon", City = "Hull"},
                new {No = 8, Name = "Henry", City = "Seattle"},
                new {No = 9, Name = "Isaac", City = "Seattle"},
                new {No = 10, Name = "James", City = "London"}
            };

            var result = from item in lst.AsParallel()
                where item.City == "Seattle"
                select item;

            result.ForAll(item => Console.WriteLine("No:{0}, Name:{1}", item.No, item.Name));
        }

        public static void Listen1_10_ExceptionsInPLINQ()
        {
            var lst = new[]
            {
                new {No = 1, Name = "Alan", City = "Hull"},
                new {No = 2, Name = "Beryl", City = "Seattle"},
                new {No = 3, Name = "Charles", City = "London"},
                new {No = 4, Name = "David", City = "Seattle"},
                new {No = 5, Name = "Eddy", City = ""},
                new {No = 6, Name = "Fred", City = ""},
                new {No = 7, Name = "Gordon", City = "Hull"},
                new {No = 8, Name = "Henry", City = "Seattle"},
                new {No = 9, Name = "Isaac", City = "Seattle"},
                new {No = 10, Name = "James", City = "London"}
            };

            Func<string, bool> func = (str) =>
            {
                if (string.IsNullOrEmpty(str))
                {
                    throw new ArgumentException(str);
                }

                return str == "Seattle";
            };

            try
            {
                var result = from item in
                        lst.AsParallel()
                    where func(item.City)
                    select item;
                result.ForAll(item => Console.WriteLine("No:{0}, Name:{1}", item.No, item.Name));
            }
            catch (AggregateException e)
            {
                Console.WriteLine(e.InnerExceptions.Count + " exceptions.");
            }
        }

        public static void Listen1_11_CreateTask()
        {
            var task = new Task(() =>
            {
                Console.WriteLine("Work starting");
                Thread.Sleep(2000);
                Console.WriteLine("Work finished");
            });

            task.Start();

            Console.WriteLine("task started");

            Console.WriteLine("task wait before");
            task.Wait();
            Console.WriteLine("task wait end");
        }

        public static void Listen1_12_RunTask()
        {
            var task = new Task(() =>
            {
                Console.WriteLine("Work starting");
                Thread.Sleep(2000);
                Console.WriteLine("Work finished");
            });

            Console.WriteLine("task wait before");

            // ここで処理が止まる
            task.Wait();
            Console.WriteLine("task wait end");
        }

        public static void Listen1_13_TaskFactory()
        {
            var act = new Action(() =>
            {
                Console.WriteLine("Work starting");
                Thread.Sleep(2000);
                Console.WriteLine("Work finished");
            });

            // Used to allow us to control the an executing task
            CancellationTokenSource cts = new CancellationTokenSource();

            // Create the task factory

            TaskFactory factory = new TaskFactory(
                cts.Token,
                TaskCreationOptions.PreferFairness,
                TaskContinuationOptions.ExecuteSynchronously,
                null); // custom scheduler could go here. 

            // Use the factory to create a new Task running do work
            Task t2 = factory.StartNew(() => act());

            Console.WriteLine("Press enter to dispose of the task");
            Console.ReadLine();

            Console.WriteLine("Disposing of the task");

            // Cancel the task
            // dispose of the task using the cancellation token
            cts.Dispose();
        }
    }
}