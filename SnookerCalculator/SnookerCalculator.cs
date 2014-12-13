using System;
using System.Collections.Generic;
using System.Linq;

namespace SnookerCalculator
{
    public class SnookerCalculator
    {
        public static IEnumerable<int> Calculate(int player1Score, int player2Score, int numRedsRemaining, int lowestColourAvailable = 2)
        {
            var losingScore = Math.Min(player1Score, player2Score);
            var winningScore = Math.Max(player1Score, player2Score);

            var remainingBalls = CalculateRemainingBalls(numRedsRemaining, lowestColourAvailable).ToList();
            var scoredBalls = new List<int>();

            foreach (var ball in remainingBalls)
            {
                if (LosingPlayerRequiresSnookers(losingScore, winningScore, remainingBalls, scoredBalls)) break;
                scoredBalls.Add(ball);
            }

            return scoredBalls;
        }

        private static bool LosingPlayerRequiresSnookers(int losingScore, int winningScore, IEnumerable<int> remainingBalls, IEnumerable<int> scoredBalls)
        {
            var remainingBallsSum = remainingBalls.Sum();
            var scoredBallsSum = scoredBalls.Sum();
            return losingScore + remainingBallsSum - scoredBallsSum < winningScore + scoredBallsSum;
        }

        private static int CalculateRemainingTotal(int numRedsRemaining, int lowestColourAvailable)
        {
            return (8 * numRedsRemaining) + RemainingColours(lowestColourAvailable).Sum();
        }

        private static IEnumerable<int> CalculateRemainingBalls(int numRedsRemaining, int lowestColourAvailable)
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
