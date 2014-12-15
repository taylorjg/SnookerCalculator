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

            frameBallDetails = AddSnookersRequired(frameBallDetails, lowestAvailableColour, currentRemainingBalls.Count);

            if (player1Score == player2Score) return AnalysisResult.Draw(frameBallDetails);

            return player1Score > player2Score
                       ? AnalysisResult.Player1Winning(frameBallDetails)
                       : AnalysisResult.Player2Winning(frameBallDetails);
        }

        private static FrameBallDetails CalculateFrameBallDetails(
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

            if (bestPossibleLosingScore >= latestWinningScore)
            {
                return null;
            }

            return new FrameBallDetails(
                frameBalls,
                latestWinningScore,
                pointsAhead,
                remainingBallsSum);
        }

        private static FrameBallDetails AddSnookersRequired(
            FrameBallDetails frameBallDetails,
            int lowestAvailableColour,
            int numRemainingBalls)
        {
            if (numRemainingBalls == 1)
            {
                return frameBallDetails;
            }

            var pointsDifference = frameBallDetails.PointsAhead - frameBallDetails.PointsRemaining;
            var valueOfSnookersNeeded = Math.Max(Ball.Brown.ToInt(), lowestAvailableColour);
            var numberOfSnookersNeeded = ((pointsDifference - 1) / valueOfSnookersNeeded) + 1;
            var canOnlyDraw = (numberOfSnookersNeeded * valueOfSnookersNeeded == pointsDifference);

            var snookersRequiredDetails = new SnookersRequiredDetails(
                numberOfSnookersNeeded,
                valueOfSnookersNeeded,
                canOnlyDraw);

            return new FrameBallDetails(
                frameBallDetails.FrameBalls,
                frameBallDetails.Score,
                frameBallDetails.PointsAhead,
                frameBallDetails.PointsRemaining,
                snookersRequiredDetails);
        }

        private static readonly int[] RedAndBlack = new[]
            {
                Ball.Red.ToInt(),
                Ball.Black.ToInt()
            };

        private static readonly int[] Colours = new[]
            {
                Ball.Yellow.ToInt(),
                Ball.Green.ToInt(),
                Ball.Brown.ToInt(),
                Ball.Blue.ToInt(),
                Ball.Pink.ToInt(),
                Ball.Black.ToInt()
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
