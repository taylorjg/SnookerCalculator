using System;
using System.Collections.Generic;
using System.Linq;

namespace SnookerCalculatorLib
{
    public class SnookerCalculator
    {
        public static AnalysisResult Analyse(int player1Score, int player2Score, int numRedsRemaining, int lowestColourAvailable = 2)
        {
            var initialLosingScore = Math.Min(player1Score, player2Score);
            var initialWinningScore = Math.Max(player1Score, player2Score);

            var remainingBallsImmutable = RemainingBalls(numRedsRemaining, lowestColourAvailable).ToList();
            var remainingBallsMutable = new List<int>(remainingBallsImmutable);
            var remainingBallsPotted = new List<int>();
            Tuple<int, int, int> scoreAheadRemaining = null;

            foreach (var remainingBall in remainingBallsImmutable)
            {
                scoreAheadRemaining = LosingPlayerCanStillWinOrDraw(
                    initialLosingScore,
                    initialWinningScore,
                    remainingBallsMutable,
                    remainingBallsPotted);

                if (scoreAheadRemaining != null)
                {
                    break;
                }

                remainingBallsMutable.RemoveAt(0);
                remainingBallsPotted.Add(remainingBall);
            }

            if (player1Score == player2Score) return AnalysisResult.Draw(remainingBallsPotted, scoreAheadRemaining);

            return player1Score > player2Score
                       ? AnalysisResult.Player1Winning(remainingBallsPotted, scoreAheadRemaining)
                       : AnalysisResult.Player2Winning(remainingBallsPotted, scoreAheadRemaining);
        }

        private static Tuple<int, int, int> LosingPlayerCanStillWinOrDraw(int initialLosingScore, int initialWinningScore, IEnumerable<int> remainingBalls, IEnumerable<int> ballsPotted)
        {
            var remainingBallsSum = remainingBalls.Sum();
            var ballsPottedSum = ballsPotted.Sum();
            var bestPossibleLosingScore = initialLosingScore + remainingBallsSum;
            var winningScore = initialWinningScore + ballsPottedSum;

            return bestPossibleLosingScore >= winningScore
                       ? null
                       : Tuple.Create(winningScore, winningScore - initialLosingScore, remainingBallsSum);
        }

        private static IEnumerable<int> RemainingBalls(int numRedsRemaining, int lowestColourAvailable)
        {
            var result = new List<int>();
            for (var i = 0; i < numRedsRemaining; i++) result.AddRange(new[] {1, 7});
            return result.Concat(RemainingColours(lowestColourAvailable));
        }

        private static IEnumerable<int> RemainingColours(int lowestColourAvailable)
        {
            return new[] { 2, 3, 4, 5, 6, 7 }.Where(b => b >= lowestColourAvailable);
        }
    }
}
