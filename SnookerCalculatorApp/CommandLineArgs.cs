namespace SnookerCalculatorApp
{
    internal class CommandLineArgs
    {
        public CommandLineArgs(int player1Score, int player2Score, int numRedsRemaining, int? lowestAvailableColour)
        {
            _player1Score = player1Score;
            _player2Score = player2Score;
            _numRedsRemaining = numRedsRemaining;
            _lowestAvailableColour = lowestAvailableColour;
        }

        public int Player1Score
        {
            get { return _player1Score; }
        }

        public int Player2Score
        {
            get { return _player2Score; }
        }

        public int NumRedsRemaining
        {
            get { return _numRedsRemaining; }
        }

        public int? LowestAvailableColour
        {
            get { return _lowestAvailableColour; }
        }

        private readonly int _player1Score;
        private readonly int _player2Score;
        private readonly int _numRedsRemaining;
        private readonly int? _lowestAvailableColour;
    }
}
