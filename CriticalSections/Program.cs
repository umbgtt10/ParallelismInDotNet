﻿using Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CriticalSections
{
    class Program
    {
        public static void SynchronizeAtBankLevelUnsafe()
        {
            var tasks = new List<Task>();
            var number = new Random();
            var bank = new BankFactory().Build(BankType.Unsafe);

            tasks.Add(Task.Factory.StartNew(() =>
            {
                for (int j = 0; j < 1000; j++)
                {
                    bank.Deposit(1);
                    Console.WriteLine($"Task: {Task.CurrentId} is depositing 1 buck.");
                }
            }));

            tasks.Add(Task.Factory.StartNew(() =>
            {
                for (int j = 0; j < 1000; j++)
                {
                    bank.Withdraw(1);
                    Console.WriteLine($"Task: {Task.CurrentId} is withdrawing 1 buck.");
                }
            }));

            Task.WaitAll(tasks.ToArray());

            Console.WriteLine($"Final balance: {bank.Balance}");
        }

        static void Main(string[] args)
        {
            SynchronizeAtBankLevelUnsafe();

            Console.ReadKey();
        }
    }
}