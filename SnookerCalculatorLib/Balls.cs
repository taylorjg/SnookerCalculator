using System;

namespace SnookerCalculatorLib
{
    public static class Balls
    {
        public const int Red = 1;
        public const int Yellow = 2;
        public const int Green = 3;
        public const int Brown = 4;
        public const int Blue = 5;
        public const int Pink = 6;
        public const int Black = 7;

        public static string ToBallName(int ball)
        {
            switch (ball)
            {
                case Red: return "Red";
                case Yellow: return "Yellow";
                case Green: return "Green";
                case Brown: return "Brown";
                case Blue: return "Blue";
                case Pink: return "Pink";
                case Black: return "Black";
            }

            throw new ArgumentException(string.Format("Unknown ball value, {0}.", ball), "ball");
        }
    }
}
