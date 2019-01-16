using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskContinuation
{
    class Program
    {
        private static void SimpleContinuationProcedure()
        {
            var task1 = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Boiling water...");
                Thread.Sleep(2000);
            });

            var task2 = task1.ContinueWith(t =>
            {
                Console.WriteLine($"Completed task {task1.Id}, pour water into the cup...");
                Thread.Sleep(2000);
            });

            task2.Wait();
            Console.WriteLine("Finished.");
            Console.ReadKey();
        }

        private static void AggregareContinueWhenAll()
        {
            var task1 = Task.Factory.StartNew(() =>
            {
                Thread.Sleep(1000);
                return "Task 1";
            });
            var task2 = Task.Factory.StartNew(() =>
            {
                Thread.Sleep(2000);
                return "Task 2";
            });

            var task3 = Task.Factory.ContinueWhenAll(new[] { task1, task2 }, tasks =>
            {
                Console.WriteLine("Tasks have completed:");
                foreach (var t in tasks)
                {
                    Console.WriteLine(" - " + t.Result);
                }
                Console.WriteLine("All tasks done!");
            });

            task3.Wait();

            Console.WriteLine("Finished.");
            Console.ReadKey();
        }

        private static void AggregareContinueWhenAny()
        {
            var task1 = Task.Factory.StartNew(() =>
            {
                Thread.Sleep(1000);
                return "Task 1";
            });
            var task2 = Task.Factory.StartNew(() =>
            {
                Thread.Sleep(1000);
                return "Task 2";
            });

            var task3 = Task.Factory.ContinueWhenAny(new[] {task1, task2}, t =>
            {
                Console.WriteLine("One task has completed:");
                Console.WriteLine(t.Result);
            });

            task3.Wait();

            Console.WriteLine("Finished.");
            Console.ReadKey();
        }


        static void Main(string[] args)
        {
            // SimpleContinuationProcedure();

            // AggregareContinueWhenAll();

            AggregareContinueWhenAny();
        }
    }
}
