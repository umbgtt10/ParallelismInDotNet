using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Monitor
{
    class Program
    {
        public static void SynchronizeAtBankLevelSpinLock()
        {
            var tasks = new List<Task>();
            var number = new Random();
            var bank = new BankFactory().Build(BankType.MonitoredBank);

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
            SynchronizeAtBankLevelSpinLock();

            Console.ReadKey();
        }
    }
}
