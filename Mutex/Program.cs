using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mutex
{
    class Program
    {
        private static void SynchronizeAtTaskLevelMutex()
        {
            var tasks = new List<Task>();
            var number = new Random();
            var bank = new UnsafeBank();
            var bank2 = new UnsafeBank();

            var mutex1 = new System.Threading.Mutex();
            var mutex2 = new System.Threading.Mutex();

            tasks.Add(Task.Factory.StartNew(() =>
            {
                for (int j = 0; j < 1000; j++)
                {
                    bool lockTaken = false;
                    try
                    {
                        lockTaken = mutex1.WaitOne();
                        bank.Deposit(1);
                    }
                    finally
                    {
                        if (lockTaken) mutex1.ReleaseMutex();
                    }

                    Console.WriteLine($"Task: {Task.CurrentId} is depositing 1 buck.");
                }
            }));

            tasks.Add(Task.Factory.StartNew(() =>
            {
                for (int j = 0; j < 1000; j++)
                {
                    bool lockTaken = false;
                    try
                    {
                        lockTaken = mutex2.WaitOne();
                        bank2.Deposit(1);
                    }
                    finally
                    {
                        if (lockTaken) mutex2.ReleaseMutex();
                    }

                    Console.WriteLine($"Task: {Task.CurrentId} is withdrawing 1 buck.");
                }
            }));

            tasks.Add(Task.Factory.StartNew(() =>
            {
                for (int j = 0; j < 1000; j++)
                {
                    bool lockTaken = WaitHandle.WaitAll(new[] { mutex1, mutex2 });
                    try
                    {
                        bank2.Transfer(bank, 1);
                    }
                    finally
                    {
                        if (lockTaken)
                        {
                            mutex1.ReleaseMutex();
                            mutex2.ReleaseMutex();
                        }
                    }

                    Console.WriteLine($"Task: {Task.CurrentId} is Transferring 1 buck.");
                }
            }));
            Task.WaitAll(tasks.ToArray());

            Console.WriteLine($"Final balance for b: {bank.Balance}");
            Console.WriteLine($"Final balance for b2: {bank2.Balance}");
        }

        static void Main(string[] args)
        {
            SynchronizeAtTaskLevelMutex();

            Console.ReadKey();
        }
    }
}
