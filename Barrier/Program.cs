using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Barriers
{
    class Program
    {
        static private Barrier _barrier = new Barrier(2, b =>
        {
            Console.WriteLine($"Phase : {b.CurrentPhaseNumber} is finished.");
        });

        private static void Water()
        {
            Console.WriteLine("Putting the kattle on (takes a while....)");
            Thread.Sleep(2000);
            _barrier.SignalAndWait();
            Console.WriteLine("Pouring water into the cup.");
            _barrier.SignalAndWait();
            Console.WriteLine("Putting the kettle away...");
        }

        private static void Cup()
        {
            Console.WriteLine("Finding a nice cup...");
            _barrier.SignalAndWait();
            Console.WriteLine("Adding tea.");
            _barrier.SignalAndWait();
            Console.WriteLine("Adding sugar.");
        }

        static void Main(string[] args)
        {
            var water = Task.Factory.StartNew(Water);
            var cup = Task.Factory.StartNew(Cup);

            var tea = Task.Factory.ContinueWhenAll(new[] {water, cup}, tasks =>
            {
                Console.WriteLine("Enjoy your cup of tea.");
            });

            Console.ReadKey();
            Console.WriteLine("Finished.");
        }
    }
}
