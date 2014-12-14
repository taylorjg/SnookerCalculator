using System;
using System.Collections.Generic;
using System.Linq;

namespace SnookerCalculatorLib
{
    public class AnalysisResult
    {
        private AnalysisResult(
            AnalysisResultType analysisResultType,
            IEnumerable<int> howToAchieveFrameBall,
            Tuple<int, int, int> scoreAheadRemaining,
            Tuple<Ball, int> snookersRequired)
        {
            _analysisResultType = analysisResultType;
            _howToAchieveFrameBall = howToAchieveFrameBall ?? Enumerable.Empty<int>();
            _scoreAheadRemaining = scoreAheadRemaining;
            _snookersRequired = snookersRequired;
        }

        public static AnalysisResult Player1Winning(IEnumerable<int> howToAchieveFrameBall, Tuple<int, int, int> scoreAheadRemaining)
        {
            return new AnalysisResult(AnalysisResultType.Player1Winning, howToAchieveFrameBall, scoreAheadRemaining, null);
        }

        public static AnalysisResult Player2Winning(IEnumerable<int> howToAchieveFrameBall, Tuple<int, int, int> scoreAheadRemaining)
        {
            return new AnalysisResult(AnalysisResultType.Player2Winning, howToAchieveFrameBall, scoreAheadRemaining, null);
        }

        public static AnalysisResult Draw(IEnumerable<int> howToAchieveFrameBall, Tuple<int, int, int> scoreAheadRemaining)
        {
            return new AnalysisResult(AnalysisResultType.Draw, howToAchieveFrameBall, scoreAheadRemaining, null);
        }

        public static AnalysisResult Player1NeedsSnookers(Tuple<Ball, int> snookersRequired)
        {
            return new AnalysisResult(AnalysisResultType.Player1NeedsSnookers, null, null, snookersRequired);
        }

        public static AnalysisResult Player2NeedsSnookers(Tuple<Ball, int> snookersRequired)
        {
            return new AnalysisResult(AnalysisResultType.Player2NeedsSnookers, null, null, snookersRequired);
        }

        public AnalysisResultType AnalysisResultType
        {
            get { return _analysisResultType; }
        }

        public IEnumerable<int> HowToAchieveFrameBall
        {
            get { return _howToAchieveFrameBall; }
        }

        public Tuple<int, int, int> ScoreAheadRemaining
        {
            get { return _scoreAheadRemaining; }
        }

        public Tuple<Ball, int> SnookersRequired
        {
            get { return _snookersRequired; }
        }

        private readonly AnalysisResultType _analysisResultType;
        private readonly IEnumerable<int> _howToAchieveFrameBall;
        private readonly Tuple<int, int, int> _scoreAheadRemaining; // 80, 64, 51
        private readonly Tuple<Ball, int> _snookersRequired;
    }
}
