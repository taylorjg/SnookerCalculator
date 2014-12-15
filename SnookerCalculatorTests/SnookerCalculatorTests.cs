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
            Assert.That(actual.FrameBallDetails.FrameBalls, Is.EqualTo(new[] {1, 7, 1, 7, 1, 7, 1, 7}));
            Assert.That(actual.FrameBallDetails.Score, Is.EqualTo(80));
            Assert.That(actual.FrameBallDetails.PointsAhead, Is.EqualTo(64));
            Assert.That(actual.FrameBallDetails.PointsRemaining, Is.EqualTo(51));
            Assert.That(actual.FrameBallDetails.SnookersRequiredDetails.NumberOfSnookersNeeded, Is.EqualTo(4));
            Assert.That(actual.FrameBallDetails.SnookersRequiredDetails.ValueOfSnookersNeeded, Is.EqualTo(4));
            Assert.That(actual.FrameBallDetails.SnookersRequiredDetails.CanOnlyDraw, Is.False);
        }

        [Test]
        public void Player2WinningAndNeeds4RedsAnd4BlacksToLeaveSnookers()
        {
            const int player1Score = (1 + 7) * 2;
            const int player2Score = (1 + 7) * 6;
            const int numRedsRemaining = 15 - 2 - 6;
            var actual = SnookerCalculator.Analyse(player1Score, player2Score, numRedsRemaining);
            Assert.That(actual.AnalysisResultType, Is.EqualTo(AnalysisResultType.Player2Winning));
            Assert.That(actual.FrameBallDetails.FrameBalls, Is.EqualTo(new[] { 1, 7, 1, 7, 1, 7, 1, 7 }));
            Assert.That(actual.FrameBallDetails.Score, Is.EqualTo(80));
            Assert.That(actual.FrameBallDetails.PointsAhead, Is.EqualTo(64));
            Assert.That(actual.FrameBallDetails.PointsRemaining, Is.EqualTo(51));
            Assert.That(actual.FrameBallDetails.SnookersRequiredDetails.NumberOfSnookersNeeded, Is.EqualTo(4));
            Assert.That(actual.FrameBallDetails.SnookersRequiredDetails.ValueOfSnookersNeeded, Is.EqualTo(4));
            Assert.That(actual.FrameBallDetails.SnookersRequiredDetails.CanOnlyDraw, Is.False);
        }

        [Test]
        public void DrawNeeds6RedsAnd6BlacksToLeaveSnookers()
        {
            const int player1Score = (1 + 7) * 4;
            const int player2Score = (1 + 7) * 4;
            const int numRedsRemaining = 15 - 4 - 4;
            var actual = SnookerCalculator.Analyse(player1Score, player2Score, numRedsRemaining);
            Assert.That(actual.AnalysisResultType, Is.EqualTo(AnalysisResultType.Draw));
            Assert.That(actual.FrameBallDetails.FrameBalls, Is.EqualTo(new[] {1, 7, 1, 7, 1, 7, 1, 7, 1, 7, 1, 7}));
            Assert.That(actual.FrameBallDetails.Score, Is.EqualTo(80));
            Assert.That(actual.FrameBallDetails.PointsAhead, Is.EqualTo(48));
            Assert.That(actual.FrameBallDetails.PointsRemaining, Is.EqualTo(35));
            Assert.That(actual.FrameBallDetails.SnookersRequiredDetails.NumberOfSnookersNeeded, Is.EqualTo(4));
            Assert.That(actual.FrameBallDetails.SnookersRequiredDetails.ValueOfSnookersNeeded, Is.EqualTo(4));
            Assert.That(actual.FrameBallDetails.SnookersRequiredDetails.CanOnlyDraw, Is.False);
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
            Assert.That(actual.FrameBallDetails.FrameBalls, Is.EqualTo(new[] {5, 6}));
            Assert.That(actual.FrameBallDetails.Score, Is.EqualTo(61));
            Assert.That(actual.FrameBallDetails.PointsAhead, Is.EqualTo(16));
            Assert.That(actual.FrameBallDetails.PointsRemaining, Is.EqualTo(7));
            Assert.That(actual.FrameBallDetails.SnookersRequiredDetails, Is.Null);
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
            Assert.That(actual.SnookersRequiredDetails.NumberOfSnookersNeeded, Is.EqualTo(2));
            Assert.That(actual.SnookersRequiredDetails.ValueOfSnookersNeeded, Is.EqualTo(5));
        }
    }
}
