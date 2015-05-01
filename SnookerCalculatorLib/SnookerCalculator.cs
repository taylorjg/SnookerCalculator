using System;
using System.Collections.Generic;
using System.Linq;

namespace SnookerCalculatorLib
{
    public class SnookerCalculator
    {
        public static AnalysisResult Analyse(
            int player1Score,
            int player2Score,
            int numRedsRemaining,
            int lowestAvailableColour = Balls.Yellow)
        {
            var losingScore = Math.Min(player1Score, player2Score);
            var winningScore = Math.Max(player1Score, player2Score);
            var remainingBalls = RemainingBalls(numRedsRemaining, lowestAvailableColour).ToList();
            var pointsAhead = winningScore - losingScore;
            var pointsRemaining = remainingBalls.Sum();

            if (pointsAhead > pointsRemaining)
            {
                return CreateSnookersRequiredDetails(
                    player1Score - player2Score,
                    pointsAhead,
                    pointsRemaining,
                    lowestAvailableColour);
            }

            return CreateFrameBallDetails(
                player1Score - player2Score,
                losingScore,
                winningScore,
                remainingBalls);
        }

        private static AnalysisResult CreateSnookersRequiredDetails(
            int scoreComparison,
            int pointsAhead,
            int pointsRemaining,
            int lowestAvailableColour)
        {
            var snookersRequiredDetails = CalculateSnookersRequired(
                pointsAhead,
                pointsRemaining,
                lowestAvailableColour);

            return (scoreComparison > 0)
                ? AnalysisResult.Player2NeedsSnookers(snookersRequiredDetails)
                : AnalysisResult.Player1NeedsSnookers(snookersRequiredDetails);
        }

        private static AnalysisResult CreateFrameBallDetails(
            int scoreComparison,
            int losingScore,
            int winningScore,
            IList<int> remainingBalls)
        {
            var frameBallDetailsForWinningPlayer = CalculateFrameBallDetails(
                CalculateFrameBallDetailsForWinningPlayer,
                losingScore,
                winningScore,
                remainingBalls);

            var frameBallDetailsForLosingPlayer = CalculateFrameBallDetails(
                CalculateFrameBallDetailsForLosingPlayer,
                losingScore,
                winningScore,
                remainingBalls);

            if (scoreComparison == 0) return AnalysisResult.Draw(
                frameBallDetailsForWinningPlayer,
                frameBallDetailsForLosingPlayer);

            return (scoreComparison > 0)
                       ? AnalysisResult.Player1Winning(
                           frameBallDetailsForWinningPlayer,
                           frameBallDetailsForLosingPlayer)
                       : AnalysisResult.Player2Winning(
                           frameBallDetailsForWinningPlayer,
                           frameBallDetailsForLosingPlayer);
        }

        private static FrameBallDetails CalculateFrameBallDetails(
            Func<int, int, IList<int>, IList<int>, FrameBallDetails> calculateFrameBallDetailsHelper,
            int losingScore,
            int winningScore,
            IList<int> initialRemainingBalls)
        {
            var currentRemainingBalls = new List<int>(initialRemainingBalls);
            var frameBalls = new List<int>();
            var frameBallDetails = null as FrameBallDetails;
            
            foreach (var remainingBall in initialRemainingBalls)
            {
                currentRemainingBalls.RemoveAt(0);
                frameBalls.Add(remainingBall);
            
                frameBallDetails = calculateFrameBallDetailsHelper(
                    losingScore,
                    winningScore,
                    currentRemainingBalls,
                    frameBalls);
            
                if (frameBallDetails != null) break;
            }
            
            if (frameBallDetails != null)
            {
                frameBallDetails = AddSnookersRequired(
                    frameBallDetails,
                    currentRemainingBalls);
            }

            return frameBallDetails;
        }

        private static FrameBallDetails CalculateFrameBallDetailsForWinningPlayer(
            int initialLosingScore,
            int initialWinningScore,
            IList<int> remainingBalls,
            IList<int> frameBalls)
        {
            var remainingBallsSum = remainingBalls.Sum();
            var frameBallsSum = frameBalls.Sum();
            var bestPossibleLosingScore = initialLosingScore + remainingBallsSum;
            var latestWinningScore = initialWinningScore + frameBallsSum;
            var pointsAhead = latestWinningScore - initialLosingScore;

            if (bestPossibleLosingScore >= latestWinningScore) return null;

            return new FrameBallDetails(
                frameBalls,
                latestWinningScore,
                pointsAhead,
                SanitisedRemainingBallsSum(remainingBalls));
        }

        private static FrameBallDetails CalculateFrameBallDetailsForLosingPlayer(
            int initialLosingScore,
            int initialWinningScore,
            IList<int> remainingBalls,
            IList<int> frameBalls)
        {
            var remainingBallsSum = remainingBalls.Sum();
            var frameBallsSum = frameBalls.Sum();
            var bestPossibleWinningScore = initialWinningScore + remainingBallsSum;
            var latestLosingScore = initialLosingScore + frameBallsSum;
            var pointsAhead = latestLosingScore - initialWinningScore;

            if (latestLosingScore < bestPossibleWinningScore) return null;

            return new FrameBallDetails(
                frameBalls,
                latestLosingScore,
                pointsAhead,
                SanitisedRemainingBallsSum(remainingBalls));
        }

        private static int SanitisedRemainingBallsSum(ICollection<int> remainingBalls)
        {
            var skipCount = (remainingBalls.Count > Colours.Count() && remainingBalls.First() != Balls.Red) ? 1 : 0;
            return remainingBalls.Skip(skipCount).Sum();
        }

        private static FrameBallDetails AddSnookersRequired(
            FrameBallDetails frameBallDetails,
            IReadOnlyCollection<int> remainingBalls)
        {
            if (!remainingBalls.Any()) return frameBallDetails;

            var lowestAvailableColour =
                (remainingBalls.Count < Colours.Length)
                    ? remainingBalls.First()
                    : Balls.Yellow;

            var snookersRequiredDetails = CalculateSnookersRequired(
                frameBallDetails.PointsAhead,
                frameBallDetails.PointsRemaining,
                lowestAvailableColour);

            return (snookersRequiredDetails != null)
                       ? new FrameBallDetails(
                             frameBallDetails.FrameBalls,
                             frameBallDetails.Score,
                             frameBallDetails.PointsAhead,
                             frameBallDetails.PointsRemaining,
                             snookersRequiredDetails)
                       : frameBallDetails;
        }

        private static SnookersRequiredDetails CalculateSnookersRequired(
            int pointsAhead,
            int pointsRemaining,
            int lowestAvailableColour)
        {
            if (lowestAvailableColour == Balls.Black) return null;

            var pointsDifference = pointsAhead - pointsRemaining;
            var valueOfSnookersNeeded = Math.Max(4, lowestAvailableColour);
            var numberOfSnookersNeeded = ((pointsDifference - 1) / valueOfSnookersNeeded) + 1;
            var toWinBy = numberOfSnookersNeeded * valueOfSnookersNeeded - pointsDifference;

            return new SnookersRequiredDetails(
                numberOfSnookersNeeded,
                valueOfSnookersNeeded,
                toWinBy);
        }

        private static readonly int[] RedAndBlack =
        {
            Balls.Red,
            Balls.Black
        };

        private static readonly int[] Colours =
        {
            Balls.Yellow,
            Balls.Green,
            Balls.Brown,
            Balls.Blue,
            Balls.Pink,
            Balls.Black
        };

        private static IEnumerable<int> RemainingBalls(int numRedsRemaining, int lowestAvailableColour)
        {
            return RedAndBlack.Repeat(numRedsRemaining).Concat(RemainingColours(lowestAvailableColour));
        }

        private static IEnumerable<int> RemainingColours(int lowestAvailableColour)
        {
            return Colours.Where(b => b >= lowestAvailableColour);
        }
    }
}
