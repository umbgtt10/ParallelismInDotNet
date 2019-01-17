using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SemaphoreSlims
{
    class Program
    {
        static void Main(string[] args)
        {
            var s = new SemaphoreSlim(2, 10);

            for (int i = 0; i < 20; i++)
            {
                Task.Factory.StartNew(() =>
                {
                    Console.WriteLine($"Entering Task :{Task.CurrentId}");
                    s.Wait(); // ReleaseCount--

                    Console.WriteLine($"Processing Task :{Task.CurrentId}");
                });
            }

            while (s.CurrentCount <= 2)
            {
                Console.WriteLine($"Semaphore count: {s.CurrentCount}");
                Console.ReadKey(); 
                s.Release(2); // ReleaseCount += 2
            }

            Console.WriteLine("Finished");
            Console.ReadKey();
        }
    }
}
