using Common;
using System.Threading;

namespace Common
{
    public class MonitoredBank : Bank
    {
        private object _monitorLock = new object();
        private int _balance;

        public int Balance
        {
            get
            {
                try
                {
                    Monitor.Enter(_monitorLock);
                    return _balance;
                }
                finally
                {
                    Monitor.Exit(_monitorLock);
                }
            }
        }

        public void Deposit(int amount)
        {
            Monitor.Enter(_monitorLock);
            _balance += amount;
            Monitor.Exit(_monitorLock);
        }

        public void Withdraw(int amount)
        {
            Monitor.Enter(_monitorLock);
            _balance -= amount;
            Monitor.Exit(_monitorLock);
        }
    }
}