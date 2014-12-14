using System.Collections.Generic;

namespace SnookerCalculatorLib
{
    public class FrameBallDetails
    {
        public FrameBallDetails(IEnumerable<int> frameBalls, int score, int pointsAhead, int pointsRemaining)
        {
            _frameBalls = frameBalls;
            _score = score;
            _pointsAhead = pointsAhead;
            _pointsRemaining = pointsRemaining;
        }

        public IEnumerable<int> FrameBalls
        {
            get { return _frameBalls; }
        }

        public int Score
        {
            get { return _score; }
        }

        public int PointsAhead
        {
            get { return _pointsAhead; }
        }

        public int PointsRemaining
        {
            get { return _pointsRemaining; }
        }

        private readonly IEnumerable<int> _frameBalls;
        private readonly int _score;
        private readonly int _pointsAhead;
        private readonly int _pointsRemaining;
    }
}
