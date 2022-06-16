using Ddd.Logic.Atms;
using Ddd.Logic.Common;

namespace Ddd.Logic.Management
{
    internal class BalanceChangedEventHandler : IHandler<BalanceChangedEvent>
    {
        public void Handle(BalanceChangedEvent entity)
        {
            // create repository
            HeadOffice headOffice = new HeadOffice();
            headOffice.ChangeBalance(entity.Delta);
            // call SaveChange
        }
    }
}
