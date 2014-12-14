namespace SnookerCalculatorLib
{
    public class SnookersRequiredDetails
    {
        public SnookersRequiredDetails(int numberOfSnookersNeeded, int valueOfSnookersNeeded)
        {
            _numberOfSnookersNeeded = numberOfSnookersNeeded;
            _valueOfSnookersNeeded = valueOfSnookersNeeded;
        }

        public int NumberOfSnookersNeeded
        {
            get { return _numberOfSnookersNeeded; }
        }

        public int ValueOfSnookersNeeded
        {
            get { return _valueOfSnookersNeeded; }
        }

        private readonly int _numberOfSnookersNeeded;
        private readonly int _valueOfSnookersNeeded;
    }
}
