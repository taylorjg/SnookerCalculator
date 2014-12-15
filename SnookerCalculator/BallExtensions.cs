using System;

namespace SnookerCalculatorLib
{
    public static class BallExtensions
    {
        public static int ToInt(this Ball ball)
        {
            return (int)ball;
        }

        public static Ball ToBall(this int ball)
        {
            return (Ball)ball;
        }

        public static string ToBallName(this Ball ball)
        {
            return Enum.GetName(typeof(Ball), ball);
        }

        public static string ToBallName(this int ball)
        {
            return ((Ball) ball).ToBallName();
        }
    }
}
