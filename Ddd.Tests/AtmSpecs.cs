using Ddd.Logic.Atms;
using Ddd.Logic.Common;
using Ddd.Logic.SharedKernel;
using FluentAssertions;
using Xunit;

namespace Ddd.Tests
{
    public class AtmSpecs
    {
        [Fact]
        public void Take_money_exchanges_money_with_commission()
        {
            Atm atm = new Atm();
            atm.LoadMoney(Money.Dollar);

            atm.TakeMoney(1m);

            atm.MoneyInSide.Amount.Should().Be(0);
            atm.MoneyCharged.Should().Be(1.01m);
        }

        [Fact]
        public void Commission_is_at_least_pne_cent()
        {
            Atm atm = new Atm();
            atm.LoadMoney(Money.Cent);

            atm.TakeMoney(0.01m);

            atm.MoneyCharged.Should().Be(0.02m);
        }

        [Fact]
        public void Commission_is_rounded_up_to_the_next_cent()
        {
            Atm atm = new Atm();
            atm.LoadMoney(Money.Dollar + Money.TenCent);

            atm.TakeMoney(1.1m);

            atm.MoneyCharged.Should().Be(1.12m);
        }
    }
}
