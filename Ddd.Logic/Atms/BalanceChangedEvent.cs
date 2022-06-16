using Ddd.Logic.Common;

namespace Ddd.Logic.Atms
{
    public class BalanceChangedEvent : IDomainEvent
    {
        public decimal Delta { get; private set; }
        public BalanceChangedEvent(decimal delta)
        {
            Delta = delta;
        }
    }
}
