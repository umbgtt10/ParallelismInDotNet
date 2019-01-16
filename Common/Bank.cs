namespace Common
{
    public interface Bank
    {
        int Balance { get; }
        void Deposit(int amount);
        void Withdraw(int amount);
    }
}