using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CountdownEvents
{
    class Program
    {
        static private int taskCount = 5;
        static private CountdownEvent _cte = new CountdownEvent(taskCount);
        static private Random _random = new Random();

        static void Main(string[] args)
        {
            for(int i =0 ;i < 5; i++)
            {
                Task.Factory.StartNew(() =>
                {
                    Console.WriteLine($"[{DateTime.Now.Ticks}] Entering task: {Task.CurrentId}");
                    Thread.Sleep(_random.Next(3000));
                    _cte.Signal();
                    Console.WriteLine($"[{DateTime.Now.Ticks}] Exiting task: {Task.CurrentId}");
                });
            }

            var task = Task.Factory.StartNew(() =>
            {
                Console.WriteLine($"[{DateTime.Now.Ticks}] Waiting for other tasks to complete in {Task.CurrentId}");
                _cte.Wait();
                Console.WriteLine($"[{DateTime.Now.Ticks}] All other tasks have completed in {Task.CurrentId}");
            });

            Console.ReadKey();
            Console.WriteLine("[{DateTime.Now.Ticks}] Finished");
        }
    }
}
