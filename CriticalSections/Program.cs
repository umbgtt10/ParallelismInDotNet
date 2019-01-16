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
        static void Main(string[] args)
        { 
            var number = new Random();
            var bank = new BankFactory().Build(BankType.LockedBank);

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
            Console.ReadKey();
            Console.WriteLine("Finished!");
        }    
    }
}