using System;

namespace CriticalSections
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
                default:
                    throw new Exception();
            }
        }
    }
}