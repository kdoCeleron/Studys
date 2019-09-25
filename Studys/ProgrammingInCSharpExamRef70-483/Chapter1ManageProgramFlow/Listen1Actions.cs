using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter1ManageProgramFlow
{
    using System.Diagnostics.CodeAnalysis;
    using System.Threading;

    /// <summary>
    /// Liesten1 の実行処理
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
    public static class Listen1Actions
    {
        /// <summary>
        /// 01 ParallellInvoke
        /// </summary>
        public static void No01ParallellInvoke()
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

        /// <summary>
        /// 02 ParallellForEach
        /// </summary>
        public static void No02ParallellForEach()
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

        /// <summary>
        /// 03 ParallellFor
        /// </summary>
        public static void No03_ParallellFor()
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

        /// <summary>
        /// 04 ParallellForLoop
        /// </summary>
        public static void No04ParallellForLoop()
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

        /// <summary>
        /// 05 ParallelLinqQuery
        /// </summary>
        public static void No05ParallelLinqQuery()
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

        /// <summary>
        /// 06 InformingParallelization
        /// </summary>
        public static void No06InformingParallelization()
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

        /// <summary>
        /// 07 Using AsOrdered
        /// </summary>
        public static void No07UsingAsOrdered()
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

        /// <summary>
        /// 08 SequentialElements
        /// </summary>
        public static void No08SequentialElements()
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

        /// <summary>
        /// 09 Using ForAll
        /// </summary>
        public static void No09UsingForAll()
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

        /// <summary>
        /// 10 ExceptionsInPLINQ
        /// </summary>
        public static void No10ExceptionsInPLINQ()
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

        /// <summary>
        /// 11 CreateTask
        /// </summary>
        public static void No11CreateTask()
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

        /// <summary>
        /// 12 unTask
        /// </summary>
        public static void No12RunTask()
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

        /// <summary>
        /// 13 TaskFactory
        /// </summary>
        public static void No13TaskFactory()
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
