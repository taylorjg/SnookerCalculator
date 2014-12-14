namespace SnookerCalculatorLib
{
    public class AnalysisResult
    {
        private AnalysisResult(
            AnalysisResultType analysisResultType,
            FrameBallDetails frameBallDetails,
            SnookersRequiredDetails snookersRequiredDetails)
        {
            _analysisResultType = analysisResultType;
            _frameBallDetails = frameBallDetails;
            _snookersRequiredDetails = snookersRequiredDetails;
        }

        public static AnalysisResult Player1Winning(FrameBallDetails frameBallDetails)
        {
            return new AnalysisResult(AnalysisResultType.Player1Winning, frameBallDetails, null);
        }

        public static AnalysisResult Player2Winning(FrameBallDetails frameBallDetails)
        {
            return new AnalysisResult(AnalysisResultType.Player2Winning, frameBallDetails, null);
        }

        public static AnalysisResult Draw(FrameBallDetails frameBallDetails)
        {
            return new AnalysisResult(AnalysisResultType.Draw, frameBallDetails, null);
        }

        public static AnalysisResult Player1NeedsSnookers(SnookersRequiredDetails snookersRequiredDetails)
        {
            return new AnalysisResult(AnalysisResultType.Player1NeedsSnookers, null, snookersRequiredDetails);
        }

        public static AnalysisResult Player2NeedsSnookers(SnookersRequiredDetails snookersRequiredDetails)
        {
            return new AnalysisResult(AnalysisResultType.Player2NeedsSnookers, null, snookersRequiredDetails);
        }

        public AnalysisResultType AnalysisResultType
        {
            get { return _analysisResultType; }
        }

        public FrameBallDetails FrameBallDetails
        {
            get { return _frameBallDetails; }
        }

        public SnookersRequiredDetails SnookersRequiredDetails
        {
            get { return _snookersRequiredDetails; }
        }

        private readonly AnalysisResultType _analysisResultType;
        private readonly FrameBallDetails _frameBallDetails;
        private readonly SnookersRequiredDetails _snookersRequiredDetails;
    }
}
