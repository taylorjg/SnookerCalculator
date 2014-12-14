namespace SnookerCalculatorLib
{
    public class SnookersRequiredDetails
    {
        public SnookersRequiredDetails(int numberOfSnookersNeeded, int valueOfSnookersNeeded, bool canOnlyDraw)
        {
            _numberOfSnookersNeeded = numberOfSnookersNeeded;
            _valueOfSnookersNeeded = valueOfSnookersNeeded;
            _canOnlyDraw = canOnlyDraw;
        }

        public int NumberOfSnookersNeeded
        {
            get { return _numberOfSnookersNeeded; }
        }

        public int ValueOfSnookersNeeded
        {
            get { return _valueOfSnookersNeeded; }
        }

        public bool CanOnlyDraw
        {
            get { return _canOnlyDraw; }
        }

        private readonly int _numberOfSnookersNeeded;
        private readonly int _valueOfSnookersNeeded;
        private readonly bool _canOnlyDraw;
    }
}
