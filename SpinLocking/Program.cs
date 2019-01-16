using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SpinLocking
{
    class Program
    {
        private static void SynchronizeAtTaskLevelSpinLock()
        {
            var tasks = new List<Task>();
            var number = new Random();
            var bank = new BankFactory().Build(BankType.Unsafe); // <== !

            var spinLock = new SpinLock();

            tasks.Add(Task.Factory.StartNew(() =>
            {
                for (int j = 0; j < 1000; j++)
                {
                    bool lockTaken = false;
                    try
                    {
                        spinLock.Enter(ref lockTaken);
                        bank.Deposit(1);
                    }
                    finally
                    {
                        if (lockTaken) spinLock.Exit();
                    }

                    Thread.Sleep(number.Next(100));
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
                        spinLock.Enter(ref lockTaken);
                        bank.Withdraw(1);
                    }
                    finally
                    {
                        if (lockTaken) spinLock.Exit();
                    }

                    Thread.Sleep(number.Next(100));
                    Console.WriteLine($"Task: {Task.CurrentId} is withdrawing 1 buck.");
                }
            }));

            Task.WaitAll(tasks.ToArray());

            Console.WriteLine($"Final balance: {bank.Balance}");
        }

        static void Main(string[] args)
        {
            SynchronizeAtTaskLevelSpinLock();

            Console.ReadKey();
            Console.WriteLine("Finished");
        }
    }
}
