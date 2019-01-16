using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CancellingTasks
{
    class Program
    {
        static void Main(string[] args)
        {
            var cts = new CancellationTokenSource();
            var token = cts.Token;

            var t = Task.Factory.StartNew(() => {
                for (int i = 0; i < 1000; i++)
                {
                    if (token.IsCancellationRequested)
                    {
                        // throws an OperationCanceledException() which stops the task.
                        // The calling thread is not notified!
                        token.ThrowIfCancellationRequested(); 
                    }
                    Console.WriteLine($"Task : {Task.CurrentId} has reached: {i}");
                    Thread.Sleep(500);
                }                
            }, token);

            Console.WriteLine("Hit any key to stop!");
            Console.ReadKey();
            cts.Cancel();

            Console.WriteLine("Finished!");
            Console.ReadKey();
        }
    }
}
