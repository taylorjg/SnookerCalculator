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
                    player2Score > player1Score,
                    pointsAhead,
                    pointsRemaining,
                    lowestAvailableColour,
                    losingScore);
            }

            return CreateFrameBallDetails(
                player1Score - player2Score,
                losingScore,
                winningScore,
                remainingBalls,
                lowestAvailableColour);
        }

        private static AnalysisResult CreateSnookersRequiredDetails(
            bool player1NeedsSnookers,
            int pointsAhead,
            int pointsRemaining,
            int lowestAvailableColour,
            int numRemainingBalls)
        {
            var snookersRequiredDetails = CalculateSnookersRequired(
                pointsAhead,
                pointsRemaining,
                lowestAvailableColour,
                numRemainingBalls);

            return (player1NeedsSnookers)
                ? AnalysisResult.Player1NeedsSnookers(snookersRequiredDetails)
                : AnalysisResult.Player2NeedsSnookers(snookersRequiredDetails);
        }

        private static AnalysisResult CreateFrameBallDetails(
            int scoreComparison,
            int losingScore,
            int winningScore,
            IList<int> remainingBalls,
            int lowestAvailableColour)
        {
            var frameBallDetailsForWinningPlayer = CalculateFrameBallDetails(
                CalculateFrameBallDetailsForWinningPlayer,
                losingScore,
                winningScore,
                remainingBalls,
                lowestAvailableColour);

            var frameBallDetailsForLosingPlayer = CalculateFrameBallDetails(
                CalculateFrameBallDetailsForLosingPlayer,
                losingScore,
                winningScore,
                remainingBalls,
                lowestAvailableColour);

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
            Func<int, int, IEnumerable<int>, IList<int>, FrameBallDetails> calculateFrameBallDetailsHelper,
            int losingScore,
            int winningScore,
            IList<int> initialRemainingBalls,
            int lowestAvailableColour)
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
                    lowestAvailableColour,
                    currentRemainingBalls.Count);
            }

            return frameBallDetails;
        }

        private static FrameBallDetails CalculateFrameBallDetailsForWinningPlayer(
            int initialLosingScore,
            int initialWinningScore,
            IEnumerable<int> remainingBalls,
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
                remainingBallsSum);
        }

        private static FrameBallDetails CalculateFrameBallDetailsForLosingPlayer(
            int initialLosingScore,
            int initialWinningScore,
            IEnumerable<int> remainingBalls,
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
                remainingBallsSum);
        }

        private static FrameBallDetails AddSnookersRequired(
            FrameBallDetails frameBallDetails,
            int lowestAvailableColour,
            int numRemainingBalls)
        {
            var snookersRequiredDetails = CalculateSnookersRequired(
                frameBallDetails.PointsAhead,
                frameBallDetails.PointsRemaining,
                lowestAvailableColour,
                numRemainingBalls);

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
            int lowestAvailableColour,
            int numRemainingBalls)
        {
            if (numRemainingBalls < 2) return null;

            var pointsDifference = pointsAhead - pointsRemaining;
            var valueOfSnookersNeeded = Math.Max(Balls.Brown, lowestAvailableColour);
            var numberOfSnookersNeeded = ((pointsDifference - 1) / valueOfSnookersNeeded) + 1;
            var toWinBy = numberOfSnookersNeeded * valueOfSnookersNeeded - pointsDifference;

            var snookersRequiredDetails = new SnookersRequiredDetails(
                numberOfSnookersNeeded,
                valueOfSnookersNeeded,
                toWinBy);

            return snookersRequiredDetails;
        }

        private static readonly int[] RedAndBlack = new[]
            {
                Balls.Red,
                Balls.Black
            };

        private static readonly int[] Colours = new[]
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
            var result = new List<int>();
            for (var i = 0; i < numRedsRemaining; i++) result.AddRange(RedAndBlack);
            return result.Concat(RemainingColours(lowestAvailableColour));
        }

        private static IEnumerable<int> RemainingColours(int lowestAvailableColour)
        {
            return Colours.Where(b => b >= lowestAvailableColour);
        }
    }
}
