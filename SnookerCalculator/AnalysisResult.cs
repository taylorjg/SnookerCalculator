using System;
using System.Collections.Generic;
using System.Linq;

namespace SnookerCalculatorLib
{
    public class AnalysisResult
    {
        private AnalysisResult(AnalysisResultType analysisResultType, IEnumerable<int> howToAchieveFrameBall, Tuple<Ball, int> snookersRequired)
        {
            _analysisResultType = analysisResultType;
            _howToAchieveFrameBall = howToAchieveFrameBall ?? Enumerable.Empty<int>();
            _snookersRequired = snookersRequired;
        }

        public static AnalysisResult Player1Winning(IEnumerable<int> howToAchieveFrameBall)
        {
            return new AnalysisResult(AnalysisResultType.Player1Winning, howToAchieveFrameBall, null);
        }

        public static AnalysisResult Player2Winning(IEnumerable<int> howToAchieveFrameBall)
        {
            return new AnalysisResult(AnalysisResultType.Player2Winning, howToAchieveFrameBall, null);
        }

        public static AnalysisResult Draw(IEnumerable<int> howToAchieveFrameBall)
        {
            return new AnalysisResult(AnalysisResultType.Draw, howToAchieveFrameBall, null);
        }

        public static AnalysisResult Player1NeedsSnookers(Tuple<Ball, int> snookersRequired)
        {
            return new AnalysisResult(AnalysisResultType.Player1NeedsSnookers, null, snookersRequired);
        }

        public static AnalysisResult Player2NeedsSnookers(Tuple<Ball, int> snookersRequired)
        {
            return new AnalysisResult(AnalysisResultType.Player2NeedsSnookers, null, snookersRequired);
        }

        public AnalysisResultType AnalysisResultType
        {
            get { return _analysisResultType; }
        }

        public IEnumerable<int> HowToAchieveFrameBall
        {
            get { return _howToAchieveFrameBall; }
        }

        public Tuple<Ball, int> SnookersRequired
        {
            get { return _snookersRequired; }
        }

        private readonly AnalysisResultType _analysisResultType;
        private readonly IEnumerable<int> _howToAchieveFrameBall;
        private readonly Tuple<Ball, int> _snookersRequired;
    }
}
