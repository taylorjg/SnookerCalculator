namespace SnookerCalculatorLib
{
    public class SnookersRequiredDetails
    {
        public SnookersRequiredDetails(int numberOfSnookersNeeded, int valueOfSnookersNeeded, int toWinBy)
        {
            _numberOfSnookersNeeded = numberOfSnookersNeeded;
            _valueOfSnookersNeeded = valueOfSnookersNeeded;
            _toWinBy = toWinBy;
        }

        public int NumberOfSnookersNeeded
        {
            get { return _numberOfSnookersNeeded; }
        }

        public int ValueOfSnookersNeeded
        {
            get { return _valueOfSnookersNeeded; }
        }

        public int ToWinBy
        {
            get { return _toWinBy; }
        }

        private readonly int _numberOfSnookersNeeded;
        private readonly int _valueOfSnookersNeeded;
        private readonly int _toWinBy;
    }
}
