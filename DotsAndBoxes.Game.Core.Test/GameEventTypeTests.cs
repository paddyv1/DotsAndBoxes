using Allure.NUnit;
using DotsAndBoxes.Game.Core.Enum;

namespace DotsAndBoxes.Game.Core.Test
{
    [AllureNUnit]
    public class GameEventTypeTests
    {
        [Test]
        public void GameEventType_ShouldHaveExpectedValues()
        {
            Assert.That(System.Enum.GetValues<GameEventType>(), Has.Length.EqualTo(4));
        }

        [Test]
        [TestCase(GameEventType.EdgePlaced, 0)]
        [TestCase(GameEventType.BoxClaimed, 1)]
        [TestCase(GameEventType.TurnChanged, 2)]
        [TestCase(GameEventType.GameOver, 3)]
        public void GameEventType_ShouldHaveCorrectUnderlyingValues(GameEventType eventType, int expectedValue)
        {
            Assert.That((int)eventType, Is.EqualTo(expectedValue));
        }

        [Test]
        public void GameEventType_ShouldContainAllExpectedMembers()
        {
            var values = System.Enum.GetNames<GameEventType>();

            Assert.That(values, Does.Contain("EdgePlaced"));
            Assert.That(values, Does.Contain("BoxClaimed"));
            Assert.That(values, Does.Contain("TurnChanged"));
            Assert.That(values, Does.Contain("GameOver"));
        }
    }
}