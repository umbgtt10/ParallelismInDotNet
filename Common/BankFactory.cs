using System;

namespace Common
{
    public partial class BankFactory
    {
        public Bank Build(BankType type)
        {
            switch (type)
            {
                case BankType.Unsafe:
                    return new UnsafeBank();
                case BankType.LockedBank:
                    return new LockedBank();
                case BankType.InterlockedBank:
                    return new InterlockedBank();
                case BankType.MonitoredBank:
                    return new MonitoredBank();
                default:
                    throw new Exception();
            }
        }
    }
}