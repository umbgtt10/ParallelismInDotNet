using Common;

namespace Common
{
    public class UnsafeBank : Bank
    {
        public int Balance { get; private set; }

        public void Deposit(int amount)
        {
            Balance += amount;
        }

        public void Withdraw(int amount)
        {
            Balance -= amount;
        }

        public void Transfer(Bank where, int amount)
        {
            where.Withdraw(amount);
            Balance += amount;
        }
    }
}