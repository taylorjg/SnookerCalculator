namespace SnookerCalculatorLib
{
    public class AnalysisResult
    {
        private AnalysisResult(
            AnalysisResultType analysisResultType,
            FrameBallDetails frameBallDetailsForWinningPlayer,
            FrameBallDetails frameBallDetailsForLosingPlayer,
            SnookersRequiredDetails snookersRequiredDetails)
        {
            _analysisResultType = analysisResultType;
            _frameBallDetailsForWinningPlayer = frameBallDetailsForWinningPlayer;
            _frameBallDetailsForLosingPlayer = frameBallDetailsForLosingPlayer;
            _snookersRequiredDetails = snookersRequiredDetails;
        }

        public static AnalysisResult Player1Winning(FrameBallDetails frameBallDetails, FrameBallDetails frameBallDetails2)
        {
            return new AnalysisResult(AnalysisResultType.Player1Winning, frameBallDetails, frameBallDetails2, null);
        }

        public static AnalysisResult Player2Winning(FrameBallDetails frameBallDetails, FrameBallDetails frameBallDetails2)
        {
            return new AnalysisResult(AnalysisResultType.Player2Winning, frameBallDetails, frameBallDetails2, null);
        }

        public static AnalysisResult Draw(FrameBallDetails frameBallDetails, FrameBallDetails frameBallDetails2)
        {
            return new AnalysisResult(AnalysisResultType.Draw, frameBallDetails, frameBallDetails2, null);
        }

        public static AnalysisResult Player1NeedsSnookers(SnookersRequiredDetails snookersRequiredDetails)
        {
            return new AnalysisResult(AnalysisResultType.Player1NeedsSnookers, null, null, snookersRequiredDetails);
        }

        public static AnalysisResult Player2NeedsSnookers(SnookersRequiredDetails snookersRequiredDetails)
        {
            return new AnalysisResult(AnalysisResultType.Player2NeedsSnookers, null, null, snookersRequiredDetails);
        }

        public AnalysisResultType AnalysisResultType
        {
            get { return _analysisResultType; }
        }

        public FrameBallDetails FrameBallDetailsForWinningPlayer
        {
            get { return _frameBallDetailsForWinningPlayer; }
        }

        public FrameBallDetails FrameBallDetailsForLosingPlayer
        {
            get { return _frameBallDetailsForLosingPlayer; }
        }

        public SnookersRequiredDetails SnookersRequiredDetails
        {
            get { return _snookersRequiredDetails; }
        }

        private readonly AnalysisResultType _analysisResultType;
        private readonly FrameBallDetails _frameBallDetailsForWinningPlayer;
        private readonly FrameBallDetails _frameBallDetailsForLosingPlayer;
        private readonly SnookersRequiredDetails _snookersRequiredDetails;
    }
}
