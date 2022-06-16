using Ddd.Logic.Atms;
using Ddd.Logic.Common;
using Ddd.Logic.SharedKernel;
using Ddd.Logic.SnackMachines;

namespace Ddd.Logic.Management
{
    public class HeadOffice : AggregateRoot
    {
        public decimal Balance { get; private set; }
        public Money Cash { get; private set; }

        public void ChangeBalance(decimal delta)
        {
            Balance += delta;
        }

        public void UnloadCashFromSnackMachine(SnackMachine snackMachine)
        {
            Money money = snackMachine.UnloadMoney();
            Cash += money;
        }

        public void LoadCashToAtm(Atm atm)
        {
            atm.LoadMoney(Cash);
            Cash = Money.None;
        }
    }
}
