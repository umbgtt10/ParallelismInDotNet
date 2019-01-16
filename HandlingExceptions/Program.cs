using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandlingExceptions
{
    class Program
    {
        public static void FaultyMultiThrededProcedure()
        {
            var t1 = Task.Factory.StartNew(() =>
            {
                throw new InvalidOperationException("InvalidOperationException") { Source = "t1" };
            });

            var t2 = Task.Factory.StartNew(() =>
            {
                throw new AccessViolationException("AccessViolationException") { Source = "t2" };
            });

            try
            {
                Task.WaitAll(t1, t2);
            }
            catch (AggregateException ae)
            {
                foreach (var e in ae.InnerExceptions)
                {
                    Console.WriteLine($"Exception: {e.Message} has been thrown by {e.Source}");
                }

                ae.Handle(e =>
                {
                    if (e is InvalidOperationException)
                    {
                        Console.WriteLine("InvalidOperationException handled inside");
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                });
            }
        }

        static void Main(string[] args)
        {
            try
            {
                FaultyMultiThrededProcedure();
            }
            catch (AggregateException ae)
            {
                ae.Handle(e =>
                {
                    if (e is AccessViolationException)
                    {
                        Console.WriteLine("AccessViolationException handled outside");
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                });
            }            

            Console.WriteLine("Finished");
            Console.ReadKey();
        }
    }
}
