using System;
using System.Threading;

namespace CriticalSections
{
    public class InterlockedBank : Bank
    {
        private int _balance;

        public int Balance
        {
            get
            {
                long balance = 0;
                Interlocked.Read(ref balance);
                return (int)balance;
            }
        }

        public void Deposit(int amount)
        {
            Interlocked.Add(ref _balance, amount);
        }

        public void Withdraw(int amount)
        {
            Interlocked.Add(ref _balance, -amount);
        }
    }
}