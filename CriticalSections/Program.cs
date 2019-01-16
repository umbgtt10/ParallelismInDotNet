using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CriticalSections
{
    class Program
    {
        public static void SynchronizeAtBankLevel()
        {
            var number = new Random();
            var bank = new BankFactory().Build(BankType.MonitoredBank);

            for (int i = 0; i < 100; i++)
            {
                Task.Factory.StartNew(() =>
                {
                    bank.Deposit(1);
                    Thread.Sleep(number.Next(100));
                    Console.WriteLine($"Task: {Task.CurrentId} is depositing 1 buck.");
                });
            }

            for (int i = 0; i < 100; i++)
            {
                Task.Factory.StartNew(() =>
                {
                    bank.Withdraw(1);
                    Thread.Sleep(number.Next(100));
                    Console.WriteLine($"Task: {Task.CurrentId} is withdrawing 1 buck.");
                });
            }

            Console.ReadKey();
            Console.WriteLine($"Final balance: {bank.Balance}");
        }


        private static void SynchronizeAtTaskLevel()
        {
            var number = new Random();
            var bank = new BankFactory().Build(BankType.Unsafe); // <== !

            var spinLock = new SpinLock();

            for (int i = 0; i < 100; i++)
            {
                Task.Factory.StartNew(() =>
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
                });
            }

            for (int i = 0; i < 100; i++)
            {
                Task.Factory.StartNew(() =>
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
                });
            }

            Console.ReadKey();
            Console.WriteLine($"Final balance: {bank.Balance}");
        }

        static void Main(string[] args)
        {
            //SynchronizeAtBankLevel();

            SynchronizeAtTaskLevel();

            Task.WaitAll();

            Console.ReadKey();
            Console.WriteLine("Finished!");
        }

    }
}