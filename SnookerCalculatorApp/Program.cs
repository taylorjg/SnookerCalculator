using System;
using System.Linq;
using SnookerCalculatorLib;

namespace SnookerCalculatorApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var player1Score = int.Parse(args[0]);
            var player2Score = int.Parse(args[1]);
            var numRedsRemaining = int.Parse(args[2]);
            var lowestAvailableColour = args.Length == 4 ? int.Parse(args[3]) : null as int?;

            var result = lowestAvailableColour.HasValue
                             ? SnookerCalculator.Analyse(player1Score, player2Score, numRedsRemaining, lowestAvailableColour.Value)
                             : SnookerCalculator.Analyse(player1Score, player2Score, numRedsRemaining);

            switch (result.AnalysisResultType)
            {
                case AnalysisResultType.Player1Winning:
                    PrintFrameBallDetails("Player1 is winning and can reach frame ball as follows:", result.FrameBallDetails);
                    break;

                case AnalysisResultType.Player2Winning:
                    PrintFrameBallDetails("Player2 is winning and can reach frame ball as follows:", result.FrameBallDetails);
                    break;

                case AnalysisResultType.Draw:
                    PrintFrameBallDetails("Players are currently tied. Either player can reach frame ball as follows:", result.FrameBallDetails);
                    break;

                case AnalysisResultType.Player1NeedsSnookers:
                    PrintSnookersRequiredDetails("Player1 needs snookers", result.SnookersRequiredDetails);
                    break;

                case AnalysisResultType.Player2NeedsSnookers:
                    PrintSnookersRequiredDetails("Player2 needs snookers", result.SnookersRequiredDetails);
                    break;
            }
        }

        private static void PrintFrameBallDetails(string message, FrameBallDetails frameBallDetails)
        {
            Console.WriteLine(message);
            Console.WriteLine("Pot the following balls: {0}", string.Join(", ", frameBallDetails.FrameBalls.Select(Balls.ToBallName)));
            Console.WriteLine("The winner's score will then be: {0}", frameBallDetails.Score);
            Console.WriteLine("The winner will then be ahead by: {0}", frameBallDetails.PointsAhead);
            Console.WriteLine("The number of points available will be: {0}", frameBallDetails.PointsRemaining);

            if (frameBallDetails.SnookersRequiredDetails != null)
            {
                PrintSnookersRequiredDetails("The other player will then require snookers", frameBallDetails.SnookersRequiredDetails);
            }
        }

        private static void PrintSnookersRequiredDetails(string message, SnookersRequiredDetails snookersRequiredDetails)
        {
            Console.WriteLine(message);
            Console.WriteLine(
                "Snookers required ({0}): {1} x {2} point snookers",
                (snookersRequiredDetails.ToWinBy > 0) ? string.Format("to win by {0}", snookersRequiredDetails.ToWinBy) : "to draw",
                snookersRequiredDetails.NumberOfSnookersNeeded,
                snookersRequiredDetails.ValueOfSnookersNeeded);
        }
    }
}
