using System.Collections.Generic;

namespace SnookerCalculatorLib
{
    public class FrameBallDetails
    {
        public FrameBallDetails(IEnumerable<int> frameBalls, int score, int pointsAhead, int pointsRemaining)
            : this(frameBalls, score, pointsAhead, pointsRemaining, null)
        {
        }

        public FrameBallDetails(
            IEnumerable<int> frameBalls,
            int score,
            int pointsAhead,
            int pointsRemaining,
            SnookersRequiredDetails snookersRequiredDetails)
        {
            _frameBalls = frameBalls;
            _score = score;
            _pointsAhead = pointsAhead;
            _pointsRemaining = pointsRemaining;
            _snookersRequiredDetails = snookersRequiredDetails;
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

        public SnookersRequiredDetails SnookersRequiredDetails
        {
            get { return _snookersRequiredDetails; }
        }

        private readonly IEnumerable<int> _frameBalls;
        private readonly int _score;
        private readonly int _pointsAhead;
        private readonly int _pointsRemaining;
        private readonly SnookersRequiredDetails _snookersRequiredDetails;
    }
}
