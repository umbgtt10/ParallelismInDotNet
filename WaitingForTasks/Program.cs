using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WaitingForTasks
{
    class Program
    {
        static void Main(string[] args)
        {
            var cts = new CancellationTokenSource();
            cts.CancelAfter(new TimeSpan(0,0,2));
            var token = cts.Token;

            var t1 = new Task(() =>
            {
                Console.WriteLine($"[{ Task.CurrentId}]:I will count for 5 seconds");

                for (int i = 0; i < 5; i++)
                {
                    Thread.Sleep(1000);
                    Console.WriteLine($"[{Task.CurrentId}]: I am counting: {i}");
                    var cancelled = token.IsCancellationRequested;
                    if (cancelled)
                    {
                        token.ThrowIfCancellationRequested();
                    }
                }

                Console.WriteLine($"[{Task.CurrentId}]: I am done!");
            }, token);

            t1.Start();

            var t2 = Task.Factory.StartNew(() => {
                Console.WriteLine($"[{Task.CurrentId}]:I will sleep for 3 seconds");
                Thread.Sleep(3000);
                Console.WriteLine($"[{Task.CurrentId}]: I am done!");
            }, token);

            // t1.Wait();
            // t2.Wait();
            // t1.Wait(token);
            // t2.Wait(token);
            // Task.WaitAll(t1, t2);
            // Task.WaitAny(t1, t2);
            // Task.WaitAll(new[] { t1, t2 }, token);
            // Task.WaitAny(new[] { t1, t2 }, token);

            Console.WriteLine("Finished");
            Console.ReadKey();
        }
    }
}
