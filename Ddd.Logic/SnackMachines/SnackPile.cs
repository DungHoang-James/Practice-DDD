using Ddd.Logic.Common;

namespace Ddd.Logic.SnackMachines
{
    public class SnackPile : ValueObject<SnackPile>
    {
        public Snack Snack { get; private set; }
        public int Quantity { get; private set; }
        public decimal Price { get; private set; }

        private SnackPile()
        {

        }

        public SnackPile(Snack snack, int quantity, decimal price)
        {
            if (quantity < 0) throw new InvalidOperationException();
            if (price < 0) throw new InvalidOperationException();
            if (price % 0.01m > 0) throw new InvalidOperationException();

            Snack = snack;
            Quantity = quantity;
            Price = price;
        }

        public SnackPile SubtractOne()
        {
            return new SnackPile(Snack, Quantity - 1, Price);
        }

        protected override bool EqualsCore(SnackPile other)
        {
            return Snack == other.Snack &&
                Quantity == other.Quantity &&
                Price == other.Price;
        }

        protected override int GetHashCodeCore()
        {
            unchecked
            {
                int hashCode = Snack.GetHashCode();
                hashCode = hashCode * 397 ^ Quantity;
                hashCode = hashCode * 397 ^ Price.GetHashCode();
                return hashCode;
            }
        }
    }
}
