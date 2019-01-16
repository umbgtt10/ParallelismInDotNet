using Common;

namespace Common
{
    public class LockedBank : Bank
    {
        private object _padlock= new object();
        private int _balance;

        public int Balance
        {
            get
            {
                lock (_padlock)
                {
                    return _balance;
                }
            }

            private set
            {
                lock (_padlock)
                {
                    _balance = value;
                }
            }
        }

        public void Deposit(int amount)
        {
            lock (_padlock)
            {
                _balance += amount;
            }
        }

        public void Withdraw(int amount)
        {
            lock (_padlock)
            {
                _balance -= amount;
            }
        }
    }
}