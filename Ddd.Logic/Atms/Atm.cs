using Ddd.Logic.Common;
using Ddd.Logic.SharedKernel;

namespace Ddd.Logic.Atms
{
    public class Atm : AggregateRoot
    {
        private const decimal CommissionRate = 0.01m;

        public Money MoneyInSide { get; private set; } = Money.None;
        public decimal MoneyCharged { get; private set; }

        public void TakeMoney(decimal amount)
        {
            if (CanTakeMoney(amount) != string.Empty)
                throw new InvalidOperationException();

            Money output = MoneyInSide.Allocate(amount);
            MoneyInSide -= output;

            decimal amountWithCommission = CalculateAmountWithCommission(amount);
            MoneyCharged += amountWithCommission;

            AddDomainEvent(new BalanceChangedEvent(amountWithCommission));
        }

        public string CanTakeMoney(decimal amount)
        {
            if (amount < 0)
                return "Invalid amount";

            if (MoneyInSide.Amount < amount)
                return "Not enough money";

            if (!MoneyInSide.CanAllocate(amount))
                return "Not enough change";

            return string.Empty;
        }

        public decimal CalculateAmountWithCommission(decimal amount)
        {
            decimal commission = amount * CommissionRate;
            decimal lessThanCent = commission % 0.01m;
            if (lessThanCent > 0)
            {
                commission = commission - lessThanCent + 0.01m;
            }

            return amount + commission;
        }

        public void LoadMoney(Money money)
        {
            MoneyInSide += money;
        }
    }
}
