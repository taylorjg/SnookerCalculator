using NUnit.Framework;

namespace SnookerCalculatorTests
{
    [TestFixture]
    class ToLeaveTheOtherPlayerNeedingASnookerTests
    {
        [Test]
        public void Test1()
        {
            const int player1Score = (1 + 7) * 6;
            const int player2Score = (1 + 7) * 2;
            const int numRedsRemaining = 7;
            var result = SnookerCalculator.SnookerCalculator.Calculate(player1Score, player2Score, numRedsRemaining);
            Assert.That(result, Is.EqualTo(new[] {1, 7, 1, 7, 1, 7, 1, 7}));
        }

        [Test]
        public void Test2()
        {
            const int player1Score = (1 + 7) * 2;
            const int player2Score = (1 + 7) * 6;
            const int numRedsRemaining = 7;
            var result = SnookerCalculator.SnookerCalculator.Calculate(player1Score, player2Score, numRedsRemaining);
            Assert.That(result, Is.EqualTo(new[] {1, 7, 1, 7, 1, 7, 1, 7}));
        }
    }
}
