using System;
using System.Collections.Generic;
using System.Linq;
using SnookerCalculatorLib;

namespace SnookerCalculatorApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var commandLineArgs = GetCommandLineArgs(args);

            var result = commandLineArgs.LowestAvailableColour.HasValue
                             ? SnookerCalculator.Analyse(commandLineArgs.Player1Score,
                                                         commandLineArgs.Player2Score,
                                                         commandLineArgs.NumRedsRemaining,
                                                         commandLineArgs.LowestAvailableColour.Value)
                             : SnookerCalculator.Analyse(commandLineArgs.Player1Score,
                                                         commandLineArgs.Player2Score,
                                                         commandLineArgs.NumRedsRemaining);

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

        private static CommandLineArgs GetCommandLineArgs(IList<string> args)
        {
            var player1Score = IntFromArgsOrInput(args, 0, "Player 1's score: ");
            var player2Score = IntFromArgsOrInput(args, 1, "Player 2's score: ");
            var numRedsRemaining = IntFromArgsOrInput(args, 2, "Number of remaining reds: ");
            var lowestAvailableColour = IntFromArgsOrInputWithDefaultValue(args, 3, "Lowest available colour [Yellow]: ", null);

            return new CommandLineArgs(player1Score, player2Score, numRedsRemaining, lowestAvailableColour);
        }

        private static int IntFromArgsOrInput(IList<string> args, int index, string prompt)
        {
            if (index < args.Count)
            {
                return int.Parse(args[index]);
            }

            for (;;)
            {
                Console.Write(prompt);
                var line = Console.ReadLine();
                if (!string.IsNullOrEmpty(line))
                {
                    return int.Parse(line);
                }
            }
        }

        private static int? IntFromArgsOrInputWithDefaultValue(IList<string> args, int index, string prompt, int? defaultValue)
        {
            if (index < args.Count)
            {
                return int.Parse(args[index]);
            }

            Console.Write(prompt);
            var line = Console.ReadLine();

            return !string.IsNullOrEmpty(line) ? int.Parse(line) : defaultValue;
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
