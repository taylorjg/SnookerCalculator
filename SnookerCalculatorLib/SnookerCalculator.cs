using System;
using System.Collections.Generic;
using System.Linq;

namespace SnookerCalculatorLib
{
    public class SnookerCalculator
    {
        public static AnalysisResult Analyse(int player1Score, int player2Score, int numRedsRemaining, int lowestAvailableColour = Balls.Yellow)
        {
            var initialLosingScore = Math.Min(player1Score, player2Score);
            var initialWinningScore = Math.Max(player1Score, player2Score);

            var initialRemainingBalls = RemainingBalls(numRedsRemaining, lowestAvailableColour).ToList();

            var pointsAhead = initialWinningScore - initialLosingScore;
            var pointsRemaining = initialRemainingBalls.Sum();

            if (pointsAhead > pointsRemaining)
            {
                var snookersRequiredDetails = CalculateSnookersRequired(
                    pointsAhead,
                    pointsRemaining,
                    lowestAvailableColour,
                    initialRemainingBalls.Count);

                return (player1Score > player2Score)
                    ? AnalysisResult.Player2NeedsSnookers(snookersRequiredDetails)
                    : AnalysisResult.Player1NeedsSnookers(snookersRequiredDetails);
            }

            var currentRemainingBalls1 = new List<int>(initialRemainingBalls);
            var frameBalls1 = new List<int>();
            FrameBallDetails frameBallDetails1 = null;

            foreach (var remainingBall in initialRemainingBalls)
            {
                currentRemainingBalls1.RemoveAt(0);
                frameBalls1.Add(remainingBall);

                frameBallDetails1 = CalculateFrameBallDetailsForWinningPlayer(
                    initialLosingScore,
                    initialWinningScore,
                    currentRemainingBalls1,
                    frameBalls1);

                if (frameBallDetails1 != null)
                {
                    break;
                }
            }

            if (frameBallDetails1 != null)
            {
                frameBallDetails1 = AddSnookersRequired(frameBallDetails1, lowestAvailableColour, currentRemainingBalls1.Count);
            }

            var currentRemainingBalls2 = new List<int>(initialRemainingBalls);
            var frameBalls2 = new List<int>();
            FrameBallDetails frameBallDetails2 = null;

            foreach (var remainingBall in initialRemainingBalls)
            {
                currentRemainingBalls2.RemoveAt(0);
                frameBalls2.Add(remainingBall);

                frameBallDetails2 = CalculateFrameBallDetailsForLosingPlayer(
                    initialLosingScore,
                    initialWinningScore,
                    currentRemainingBalls2,
                    frameBalls2);

                if (frameBallDetails2 != null)
                {
                    break;
                }
            }

            if (frameBallDetails2 != null)
            {
                frameBallDetails2 = AddSnookersRequired(frameBallDetails2, lowestAvailableColour, currentRemainingBalls2.Count);
            }

            if (player1Score == player2Score) return AnalysisResult.Draw(frameBallDetails1, frameBallDetails2);

            return player1Score > player2Score
                       ? AnalysisResult.Player1Winning(frameBallDetails1, frameBallDetails2)
                       : AnalysisResult.Player2Winning(frameBallDetails1, frameBallDetails2);
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

            if (latestLosingScore < bestPossibleWinningScore)
            {
                return null;
            }

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
            if (numRemainingBalls < 2)
            {
                return null;
            }

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
