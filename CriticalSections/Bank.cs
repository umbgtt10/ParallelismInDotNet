namespace CriticalSections
{
    public interface Bank
    {
        int Balance { get; }
        void Deposit(int amount);
        void Withdraw(int amount);
    }
}