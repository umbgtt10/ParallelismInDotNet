using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ManualAndAutoResetEvents
{
    class Program
    {
        private static void SimpleManualResetEventSlimExample()
        {
            var evt = new ManualResetEventSlim();

            Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Boiling water...");
                Thread.Sleep(2000);
                evt.Set();
            });

            var makeTea = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Waiting for water....");
                evt.Wait();
                Console.WriteLine("Here is your tea....");
                evt.Wait(); // irrelevant
                evt.Wait(); // irrelevant
                evt.Wait(); // irrelevant
            });

            makeTea.Wait();

            Console.ReadKey();
            Console.WriteLine("Finished");
        }

        private static void SimpleAutoResetStuckExample()
        {
            var evt = new AutoResetEvent(false);

            Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Boiling water...");
                Thread.Sleep(2000);
                evt.Set();
            });

            var makeTea = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Waiting for water....");
                evt.WaitOne();
                Console.WriteLine("Here is your tea.... => You are stuck now!");
                evt.WaitOne(); // Blocking!
                Console.WriteLine("That was good!");
            });

            makeTea.Wait();

            Console.ReadKey();
            Console.WriteLine("Finished");
        }

        private static void SimpleAutoResetTimeOutExample()
        {
            var evt = new AutoResetEvent(false);

            Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Boiling water...");
                Thread.Sleep(2000);
                evt.Set();
            });

            var makeTea = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Waiting for water....");
                evt.WaitOne();
                Console.WriteLine("Here is your tea.... => You are NOT stuck now!");
                if (evt.WaitOne(2000))
                {
                    Console.WriteLine("That was good!");
                }
                else
                {
                    Console.WriteLine("I am not drinking it!");
                }                
            });

            makeTea.Wait();

            Console.ReadKey();
            Console.WriteLine("Finished");
        }

        static void Main(string[] args)
        {
            SimpleAutoResetTimeOutExample();
        }
    }
}
