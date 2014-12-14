using System;
using System.Collections.Generic;
using System.Linq;

namespace SnookerCalculatorLib
{
    public class SnookerCalculator
    {
        public static AnalysisResult Analyse(int player1Score, int player2Score, int numRedsRemaining, int lowestAvailableColour = 2)
        {
            var initialLosingScore = Math.Min(player1Score, player2Score);
            var initialWinningScore = Math.Max(player1Score, player2Score);

            var initialRemainingBalls = RemainingBalls(numRedsRemaining, lowestAvailableColour).ToList();
            var currentRemainingBalls = new List<int>(initialRemainingBalls);
            var frameBalls = new List<int>();
            FrameBallDetails frameBallDetails = null;

            foreach (var remainingBall in initialRemainingBalls)
            {
                frameBallDetails = CalculateFrameBallDetails(
                    initialLosingScore,
                    initialWinningScore,
                    currentRemainingBalls,
                    frameBalls);

                if (frameBallDetails != null)
                {
                    break;
                }

                currentRemainingBalls.RemoveAt(0);
                frameBalls.Add(remainingBall);
            }

            if (player1Score == player2Score) return AnalysisResult.Draw(frameBallDetails);

            return player1Score > player2Score
                       ? AnalysisResult.Player1Winning(frameBallDetails)
                       : AnalysisResult.Player2Winning(frameBallDetails);
        }

        private static FrameBallDetails CalculateFrameBallDetails(int initialLosingScore, int initialWinningScore, IEnumerable<int> remainingBalls, IList<int> frameBalls)
        {
            var remainingBallsSum = remainingBalls.Sum();
            var frameBallsSum = frameBalls.Sum();
            var bestPossibleLosingScore = initialLosingScore + remainingBallsSum;
            var projectedWinningScore = initialWinningScore + frameBallsSum;
            var pointsAhead = projectedWinningScore - initialLosingScore;

            return bestPossibleLosingScore < projectedWinningScore
                       ? new FrameBallDetails(frameBalls, projectedWinningScore, pointsAhead, remainingBallsSum)
                       : null;
        }

        private static IEnumerable<int> RemainingBalls(int numRedsRemaining, int lowestAvailableColour)
        {
            var result = new List<int>();
            for (var i = 0; i < numRedsRemaining; i++) result.AddRange(new[] {1, 7});
            return result.Concat(RemainingColours(lowestAvailableColour));
        }

        private static IEnumerable<int> RemainingColours(int lowestAvailableColour)
        {
            return new[] { 2, 3, 4, 5, 6, 7 }.Where(b => b >= lowestAvailableColour);
        }
    }
}
