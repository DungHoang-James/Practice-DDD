﻿using Ddd.Logic.Common;
using Ddd.Logic.SharedKernel;

namespace Ddd.Logic.SnackMachines
{
    public class SnackMachine : AggregateRoot
    {
        public Money MoneyInside { get; private set; }
        public decimal MoneyInTransaction { get; private set; }
        protected List<Slot> Slots { get; private set; }

        public SnackMachine()
        {
            MoneyInside = Money.None;
            MoneyInTransaction = 0;
            Slots = new List<Slot>
            {
                new Slot(this, 1),
                new Slot(this, 2),
                new Slot(this, 3),
            };
        }

        public Money UnloadMoney()
        {
            return MoneyInside;
        }

        public SnackPile GetSnackPile(int position)
        {
            return GetSlot(position).SnackPile;
        }

        public IReadOnlyList<SnackPile> GetAllSnackpile()
        {
            return Slots.OrderBy(s => s.Position)
                .Select(s => s.SnackPile)
                .ToList();
        }

        private Slot GetSlot(int position)
        {
            return Slots.Single(x => x.Position == position);
        }

        public void InsertMoney(Money money)
        {
            Money[] coinsAndNotes =
            {
                Money.Cent, Money.TenCent, Money.Quarter, Money.Dollar, Money.FiveDollar, Money.TwentyDollar
            };

            if (!coinsAndNotes.Contains(money))
                throw new InvalidOperationException();

            MoneyInTransaction += money.Amount;
            MoneyInside += money;
        }

        public void ReturnMoney()
        {
            Money moneyToReturn = MoneyInside.Allocate(MoneyInTransaction);
            MoneyInside -= moneyToReturn;
            MoneyInTransaction = 0;
        }

        public string CanBuySnack(int position)
        {
            SnackPile snackPile = GetSnackPile(position);

            if (snackPile.Quantity == 0)
                return "The snack pile is empty";

            if (MoneyInTransaction < snackPile.Price)
                return "Not enough money";

            if (!MoneyInside.CanAllocate(MoneyInTransaction - snackPile.Price))
                return "Not enough change";

            return string.Empty;
        }

        public void BuySnack(int position)
        {
            if (CanBuySnack(position) != string.Empty)
                throw new InvalidOperationException();

            Slot slot = GetSlot(position);
            slot.SnackPile = slot.SnackPile.SubtractOne();

            Money change = MoneyInside.Allocate(MoneyInTransaction - slot.SnackPile.Price);
            MoneyInside -= change;

            MoneyInTransaction = 0;
        }

        public void LoadSnacks(int position, SnackPile snackPile)
        {
            Slot slot = GetSlot(position);
            slot.SnackPile = snackPile;
        }

        public void LoadMoney(Money money)
        {
            MoneyInside += money;
        }
    }
}
