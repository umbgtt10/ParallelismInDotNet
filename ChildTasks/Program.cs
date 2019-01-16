using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChildTasks
{
    class Program
    {
        private static void SimpleDetachedParentChildTask()
        {
            var parent = new Task(() =>
            {
                var child = new Task(() =>
                {
                    Console.WriteLine("Child task starting");
                    Thread.Sleep(3000);
                    Console.WriteLine("Child task finishing");
                });

                child.Start();
            });

            parent.Start();

            try
            {
                parent.Wait();
            }
            catch (AggregateException ae)
            {
                ae.Handle(e => { return true; });
            }

            Console.WriteLine("Finished");
            Console.ReadKey();
        }

        private static void SimpleChildAttachedToParentTask()
        {
            var parent = new Task(() =>
            {
                var child = new Task(() =>
                {
                    Console.WriteLine("Child task starting");
                    Thread.Sleep(3000);
                    Console.WriteLine("Child task finishing");
                }, TaskCreationOptions.AttachedToParent);

                child.Start();
            });

            parent.Start();

            try
            {
                parent.Wait();
            }
            catch (AggregateException ae)
            {
                ae.Handle(e => { return true; });
            }

            Console.WriteLine("Finished");
            Console.ReadKey();
        }

        private static void SimpleChildAttachedToParentTaskWithContinuationHandlers()
        {
            var parent = new Task(() =>
            {
                var child = new Task(() =>
                {
                    Console.WriteLine("Child task starting");
                    Thread.Sleep(3000);
                    //throw new Exception();
                    Console.WriteLine("Child task finishing");
                }, TaskCreationOptions.AttachedToParent);

                var successHandler = child.ContinueWith(t =>
                {
                    Console.WriteLine($"OK! Task: {t.Id} completed with status: {t.Status}");
                },TaskContinuationOptions.AttachedToParent | 
                  TaskContinuationOptions.OnlyOnRanToCompletion);

                var failureHandler = child.ContinueWith(t =>
                    {
                        Console.WriteLine($"NOK! Task: {t.Id} completed with status: {t.Status}");
                    }, TaskContinuationOptions.AttachedToParent |
                       TaskContinuationOptions.OnlyOnFaulted);

                child.Start();
            });

            parent.Start();

            try
            {
                parent.Wait();
            }
            catch (AggregateException ae)
            {
                ae.Handle(e => { return true; });
            }

            Console.WriteLine("Finished");
            Console.ReadKey();
        }

        static void Main(string[] args)
        {
            // SimpleDetachedParentChildTask();

            // SimpleChildAttachedToParentTask();

            SimpleChildAttachedToParentTaskWithContinuationHandlers();
        }
    }
}
