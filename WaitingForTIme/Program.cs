using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WaitingForTime
{
    class Program
    {
        static void Main(string[] args)
        {
            var cts = new CancellationTokenSource();
            var token = cts.Token;

            var t = new Task(() =>
            {
                Console.WriteLine("Bomb is going to go off in 5 seconds");
                var cancelled = token.WaitHandle.WaitOne(5000);
                Console.WriteLine(cancelled ? "Bomb disarmed." : "BOOOOM!!!!!!!");
            }, token);
            t.Start();

            Thread.Sleep(1000);
            Console.WriteLine("Hit a key to disarm!");
            Console.ReadKey();
            cts.Cancel();

            Console.ReadKey();
            Console.WriteLine("Finished.");
        }
    }
}
