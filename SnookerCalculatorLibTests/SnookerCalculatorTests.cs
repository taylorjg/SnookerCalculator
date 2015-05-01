using NUnit.Framework;
using SnookerCalculatorLib;

namespace SnookerCalculatorLibTests
{
    [TestFixture]
    class SnookerCalculatorTests
    {
        [Test]
        public void Player1WinningAndNeeds4RedsAnd4BlacksToLeaveSnookers()
        {
            const int player1Score = (1 + 7) * 6;
            const int player2Score = (1 + 7) * 2;
            const int numRedsRemaining = 15 - 6 - 2;
            var actual = SnookerCalculator.Analyse(player1Score, player2Score, numRedsRemaining);

            Assert.That(actual.AnalysisResultType, Is.EqualTo(AnalysisResultType.Player1Winning));

            Assert.That(actual.FrameBallDetailsForWinningPlayer.FrameBalls, Is.EqualTo(new[] {1, 7, 1, 7, 1, 7, 1, 7}));
            Assert.That(actual.FrameBallDetailsForWinningPlayer.Score, Is.EqualTo(80));
            Assert.That(actual.FrameBallDetailsForWinningPlayer.PointsAhead, Is.EqualTo(64));
            Assert.That(actual.FrameBallDetailsForWinningPlayer.PointsRemaining, Is.EqualTo(51));
            Assert.That(actual.FrameBallDetailsForWinningPlayer.SnookersRequiredDetails.NumberOfSnookersNeeded, Is.EqualTo(4));
            Assert.That(actual.FrameBallDetailsForWinningPlayer.SnookersRequiredDetails.ValueOfSnookersNeeded, Is.EqualTo(4));
            Assert.That(actual.FrameBallDetailsForWinningPlayer.SnookersRequiredDetails.ToWinBy, Is.EqualTo(3));

            Assert.That(actual.FrameBallDetailsForLosingPlayer.FrameBalls, Is.EqualTo(new[] {1, 7, 1, 7, 1, 7, 1, 7, 1, 7, 1, 7, 1, 7, 2}));
            Assert.That(actual.FrameBallDetailsForLosingPlayer.Score, Is.EqualTo(74));
            Assert.That(actual.FrameBallDetailsForLosingPlayer.PointsAhead, Is.EqualTo(26));
            Assert.That(actual.FrameBallDetailsForLosingPlayer.PointsRemaining, Is.EqualTo(25));
            Assert.That(actual.FrameBallDetailsForLosingPlayer.SnookersRequiredDetails.NumberOfSnookersNeeded, Is.EqualTo(1));
            Assert.That(actual.FrameBallDetailsForLosingPlayer.SnookersRequiredDetails.ValueOfSnookersNeeded, Is.EqualTo(4));
            Assert.That(actual.FrameBallDetailsForLosingPlayer.SnookersRequiredDetails.ToWinBy, Is.EqualTo(3));

            Assert.That(actual.SnookersRequiredDetails, Is.Null);
        }

        [Test]
        public void Player2WinningAndNeeds4RedsAnd4BlacksToLeaveSnookers()
        {
            const int player1Score = (1 + 7) * 2;
            const int player2Score = (1 + 7) * 6;
            const int numRedsRemaining = 15 - 2 - 6;
            var actual = SnookerCalculator.Analyse(player1Score, player2Score, numRedsRemaining);

            Assert.That(actual.AnalysisResultType, Is.EqualTo(AnalysisResultType.Player2Winning));

            Assert.That(actual.FrameBallDetailsForWinningPlayer.FrameBalls, Is.EqualTo(new[] { 1, 7, 1, 7, 1, 7, 1, 7 }));
            Assert.That(actual.FrameBallDetailsForWinningPlayer.Score, Is.EqualTo(80));
            Assert.That(actual.FrameBallDetailsForWinningPlayer.PointsAhead, Is.EqualTo(64));
            Assert.That(actual.FrameBallDetailsForWinningPlayer.PointsRemaining, Is.EqualTo(51));
            Assert.That(actual.FrameBallDetailsForWinningPlayer.SnookersRequiredDetails.NumberOfSnookersNeeded, Is.EqualTo(4));
            Assert.That(actual.FrameBallDetailsForWinningPlayer.SnookersRequiredDetails.ValueOfSnookersNeeded, Is.EqualTo(4));
            Assert.That(actual.FrameBallDetailsForWinningPlayer.SnookersRequiredDetails.ToWinBy, Is.EqualTo(3));

            Assert.That(actual.FrameBallDetailsForLosingPlayer.FrameBalls, Is.EqualTo(new[] {1, 7, 1, 7, 1, 7, 1, 7, 1, 7, 1, 7, 1, 7, 2}));
            Assert.That(actual.FrameBallDetailsForLosingPlayer.Score, Is.EqualTo(74));
            Assert.That(actual.FrameBallDetailsForLosingPlayer.PointsAhead, Is.EqualTo(26));
            Assert.That(actual.FrameBallDetailsForLosingPlayer.PointsRemaining, Is.EqualTo(25));
            Assert.That(actual.FrameBallDetailsForLosingPlayer.SnookersRequiredDetails.NumberOfSnookersNeeded, Is.EqualTo(1));
            Assert.That(actual.FrameBallDetailsForLosingPlayer.SnookersRequiredDetails.ValueOfSnookersNeeded, Is.EqualTo(4));
            Assert.That(actual.FrameBallDetailsForLosingPlayer.SnookersRequiredDetails.ToWinBy, Is.EqualTo(3));

            Assert.That(actual.SnookersRequiredDetails, Is.Null);
        }

        [Test]
        public void DrawNeeds6RedsAnd6BlacksToLeaveSnookers()
        {
            const int player1Score = (1 + 7) * 4;
            const int player2Score = (1 + 7) * 4;
            const int numRedsRemaining = 15 - 4 - 4;
            var actual = SnookerCalculator.Analyse(player1Score, player2Score, numRedsRemaining);

            Assert.That(actual.AnalysisResultType, Is.EqualTo(AnalysisResultType.Draw));

            Assert.That(actual.FrameBallDetailsForWinningPlayer.FrameBalls, Is.EqualTo(new[] {1, 7, 1, 7, 1, 7, 1, 7, 1, 7, 1, 7}));
            Assert.That(actual.FrameBallDetailsForWinningPlayer.Score, Is.EqualTo(80));
            Assert.That(actual.FrameBallDetailsForWinningPlayer.PointsAhead, Is.EqualTo(48));
            Assert.That(actual.FrameBallDetailsForWinningPlayer.PointsRemaining, Is.EqualTo(35));
            Assert.That(actual.FrameBallDetailsForWinningPlayer.SnookersRequiredDetails.NumberOfSnookersNeeded, Is.EqualTo(4));
            Assert.That(actual.FrameBallDetailsForWinningPlayer.SnookersRequiredDetails.ValueOfSnookersNeeded, Is.EqualTo(4));
            Assert.That(actual.FrameBallDetailsForWinningPlayer.SnookersRequiredDetails.ToWinBy, Is.EqualTo(3));

            Assert.That(actual.FrameBallDetailsForLosingPlayer.FrameBalls, Is.EqualTo(new[] { 1, 7, 1, 7, 1, 7, 1, 7, 1, 7, 1, 7 }));
            Assert.That(actual.FrameBallDetailsForLosingPlayer.Score, Is.EqualTo(80));
            Assert.That(actual.FrameBallDetailsForLosingPlayer.PointsAhead, Is.EqualTo(48));
            Assert.That(actual.FrameBallDetailsForLosingPlayer.PointsRemaining, Is.EqualTo(35));
            Assert.That(actual.FrameBallDetailsForLosingPlayer.SnookersRequiredDetails.NumberOfSnookersNeeded, Is.EqualTo(4));
            Assert.That(actual.FrameBallDetailsForLosingPlayer.SnookersRequiredDetails.ValueOfSnookersNeeded, Is.EqualTo(4));
            Assert.That(actual.FrameBallDetailsForLosingPlayer.SnookersRequiredDetails.ToWinBy, Is.EqualTo(3));

            Assert.That(actual.SnookersRequiredDetails, Is.Null);
        }

        [Test]
        public void Player1OnlyNeedsBlueAndPink()
        {
            const int player1Score = 50;
            const int player2Score = 45;
            const int numRedsRemaining = 0;
            const int lowestAvailableColour = 5;
            var actual = SnookerCalculator.Analyse(player1Score, player2Score, numRedsRemaining, lowestAvailableColour);

            Assert.That(actual.AnalysisResultType, Is.EqualTo(AnalysisResultType.Player1Winning));

            Assert.That(actual.FrameBallDetailsForWinningPlayer.FrameBalls, Is.EqualTo(new[] {5, 6}));
            Assert.That(actual.FrameBallDetailsForWinningPlayer.Score, Is.EqualTo(61));
            Assert.That(actual.FrameBallDetailsForWinningPlayer.PointsAhead, Is.EqualTo(16));
            Assert.That(actual.FrameBallDetailsForWinningPlayer.PointsRemaining, Is.EqualTo(7));
            Assert.That(actual.FrameBallDetailsForWinningPlayer.SnookersRequiredDetails, Is.Null);

            Assert.That(actual.FrameBallDetailsForLosingPlayer.FrameBalls, Is.EqualTo(new[] {5, 6, 7}));
            Assert.That(actual.FrameBallDetailsForLosingPlayer.Score, Is.EqualTo(63));
            Assert.That(actual.FrameBallDetailsForLosingPlayer.PointsAhead, Is.EqualTo(13));
            Assert.That(actual.FrameBallDetailsForLosingPlayer.PointsRemaining, Is.EqualTo(0));
            Assert.That(actual.FrameBallDetailsForLosingPlayer.SnookersRequiredDetails, Is.Null);

            Assert.That(actual.SnookersRequiredDetails, Is.Null);
        }

        [Test]
        public void AlexHigginsJimmyWhite1982()
        {
            const int player1Score = 0;
            const int player2Score = 59;
            const int numRedsRemaining = 6;
            var actual = SnookerCalculator.Analyse(player1Score, player2Score, numRedsRemaining);

            Assert.That(actual.AnalysisResultType, Is.EqualTo(AnalysisResultType.Player2Winning));

            Assert.That(actual.FrameBallDetailsForWinningPlayer.FrameBalls, Is.EqualTo(new[] { 1, 7, 1 }));
            Assert.That(actual.FrameBallDetailsForWinningPlayer.Score, Is.EqualTo(68));
            Assert.That(actual.FrameBallDetailsForWinningPlayer.PointsAhead, Is.EqualTo(68));
            Assert.That(actual.FrameBallDetailsForWinningPlayer.PointsRemaining, Is.EqualTo(59));
            Assert.That(actual.FrameBallDetailsForWinningPlayer.SnookersRequiredDetails.NumberOfSnookersNeeded, Is.EqualTo(3));
            Assert.That(actual.FrameBallDetailsForWinningPlayer.SnookersRequiredDetails.ValueOfSnookersNeeded, Is.EqualTo(4));
            Assert.That(actual.FrameBallDetailsForWinningPlayer.SnookersRequiredDetails.ToWinBy, Is.EqualTo(3));

            Assert.That(actual.FrameBallDetailsForLosingPlayer.FrameBalls, Is.EqualTo(new[] { 1, 7, 1, 7, 1, 7, 1, 7, 1, 7, 1, 7, 2, 3, 4, 5, 6 }));
            Assert.That(actual.FrameBallDetailsForLosingPlayer.Score, Is.EqualTo(68));
            Assert.That(actual.FrameBallDetailsForLosingPlayer.PointsAhead, Is.EqualTo(9));
            Assert.That(actual.FrameBallDetailsForLosingPlayer.PointsRemaining, Is.EqualTo(7));
            Assert.That(actual.FrameBallDetailsForLosingPlayer.SnookersRequiredDetails, Is.Null);

            Assert.That(actual.SnookersRequiredDetails, Is.Null);
        }

        [Test]
        public void JimmyWhiteAlexHiggins1982()
        {
            const int player1Score = 59;
            const int player2Score = 0;
            const int numRedsRemaining = 6;
            var actual = SnookerCalculator.Analyse(player1Score, player2Score, numRedsRemaining);

            Assert.That(actual.AnalysisResultType, Is.EqualTo(AnalysisResultType.Player1Winning));

            Assert.That(actual.FrameBallDetailsForWinningPlayer.FrameBalls, Is.EqualTo(new[] { 1, 7, 1 }));
            Assert.That(actual.FrameBallDetailsForWinningPlayer.Score, Is.EqualTo(68));
            Assert.That(actual.FrameBallDetailsForWinningPlayer.PointsAhead, Is.EqualTo(68));
            Assert.That(actual.FrameBallDetailsForWinningPlayer.PointsRemaining, Is.EqualTo(59));
            Assert.That(actual.FrameBallDetailsForWinningPlayer.SnookersRequiredDetails.NumberOfSnookersNeeded, Is.EqualTo(3));
            Assert.That(actual.FrameBallDetailsForWinningPlayer.SnookersRequiredDetails.ValueOfSnookersNeeded, Is.EqualTo(4));
            Assert.That(actual.FrameBallDetailsForWinningPlayer.SnookersRequiredDetails.ToWinBy, Is.EqualTo(3));

            Assert.That(actual.FrameBallDetailsForLosingPlayer.FrameBalls, Is.EqualTo(new[] { 1, 7, 1, 7, 1, 7, 1, 7, 1, 7, 1, 7, 2, 3, 4, 5, 6 }));
            Assert.That(actual.FrameBallDetailsForLosingPlayer.Score, Is.EqualTo(68));
            Assert.That(actual.FrameBallDetailsForLosingPlayer.PointsAhead, Is.EqualTo(9));
            Assert.That(actual.FrameBallDetailsForLosingPlayer.PointsRemaining, Is.EqualTo(7));
            Assert.That(actual.FrameBallDetailsForLosingPlayer.SnookersRequiredDetails, Is.Null);

            Assert.That(actual.SnookersRequiredDetails, Is.Null);
        }

        [Test]
        public void Player1NeedsSnookers()
        {
            const int player1Score = 35;
            const int player2Score = 60;
            const int numRedsRemaining = 0;
            const int lowestAvailableColour = 5;
            var actual = SnookerCalculator.Analyse(player1Score, player2Score, numRedsRemaining, lowestAvailableColour);
            Assert.That(actual.AnalysisResultType, Is.EqualTo(AnalysisResultType.Player1NeedsSnookers));
            Assert.That(actual.FrameBallDetailsForWinningPlayer, Is.Null);
            Assert.That(actual.FrameBallDetailsForLosingPlayer, Is.Null);
            Assert.That(actual.SnookersRequiredDetails.NumberOfSnookersNeeded, Is.EqualTo(2));
            Assert.That(actual.SnookersRequiredDetails.ValueOfSnookersNeeded, Is.EqualTo(5));
        }

        [Test]
        public void Player2NeedsSnookers()
        {
            const int player1Score = 60;
            const int player2Score = 35;
            const int numRedsRemaining = 0;
            const int lowestAvailableColour = 5;
            var actual = SnookerCalculator.Analyse(player1Score, player2Score, numRedsRemaining, lowestAvailableColour);
            Assert.That(actual.AnalysisResultType, Is.EqualTo(AnalysisResultType.Player2NeedsSnookers));
            Assert.That(actual.FrameBallDetailsForWinningPlayer, Is.Null);
            Assert.That(actual.FrameBallDetailsForLosingPlayer, Is.Null);
            Assert.That(actual.SnookersRequiredDetails.NumberOfSnookersNeeded, Is.EqualTo(2));
            Assert.That(actual.SnookersRequiredDetails.ValueOfSnookersNeeded, Is.EqualTo(5));
        }

        [Test, Description("This test currently fails due to a bug in the implementation")]
        public void Player2WinningAndNeedsGreenBrownBlueToLeaveOtherPlayerNeedingTwoPinkSnookers()
        {
            const int player1Score = 43;
            const int player2Score = 51;
            const int numRedsRemaining = 0;
            const int lowestAvailableColour = 3;
            var actual = SnookerCalculator.Analyse(player1Score, player2Score, numRedsRemaining, lowestAvailableColour);

            Assert.That(actual.AnalysisResultType, Is.EqualTo(AnalysisResultType.Player2Winning));

            Assert.That(actual.FrameBallDetailsForWinningPlayer.FrameBalls, Is.EqualTo(new[] { 3, 4, 5 }));
            Assert.That(actual.FrameBallDetailsForWinningPlayer.Score, Is.EqualTo(63));
            Assert.That(actual.FrameBallDetailsForWinningPlayer.PointsAhead, Is.EqualTo(20));
            Assert.That(actual.FrameBallDetailsForWinningPlayer.PointsRemaining, Is.EqualTo(13));
            Assert.That(actual.FrameBallDetailsForWinningPlayer.SnookersRequiredDetails.NumberOfSnookersNeeded, Is.EqualTo(2));
            Assert.That(actual.FrameBallDetailsForWinningPlayer.SnookersRequiredDetails.ValueOfSnookersNeeded, Is.EqualTo(6));
            Assert.That(actual.FrameBallDetailsForWinningPlayer.SnookersRequiredDetails.ToWinBy, Is.EqualTo(5));

            Assert.That(actual.FrameBallDetailsForLosingPlayer.FrameBalls, Is.EqualTo(new[] {3, 4, 5, 6}));
            Assert.That(actual.FrameBallDetailsForLosingPlayer.Score, Is.EqualTo(61));
            Assert.That(actual.FrameBallDetailsForLosingPlayer.PointsAhead, Is.EqualTo(10));
            Assert.That(actual.FrameBallDetailsForLosingPlayer.PointsRemaining, Is.EqualTo(7));
            Assert.That(actual.FrameBallDetailsForLosingPlayer.SnookersRequiredDetails, Is.Null);

            Assert.That(actual.SnookersRequiredDetails, Is.Null);
        }

        // Add further tests:
        // - a test where can only draw with snookers
        // - a test where can't have snookers because only the black is left
        // - invalid scores e.g. < 0 or > some sensible limit e.g. 200
        // - invalid number of reds remaining e.g. < 1 or > 15
        // - invalid lowest available colour e.g. < 2 or > 7
        // - illogical lowest available colour e.g. != 2 when number of reds remaining is > 0
        // - etc
    }
}
