using Ddd.Logic.Common;

namespace Ddd.Logic.SnackMachines
{
    public class Slot : Entity
    {
        public SnackPile SnackPile { get; set; }
        public SnackMachine SnackMachine { get; private set; }
        public int Position { get; private set; }

        private Slot()
        {

        }

        public Slot(SnackMachine snackMachine, int position)
        {
            SnackMachine = snackMachine;
            Position = position;
            SnackPile = new SnackPile(null, 0, 0);
        }
    }
}
