using Allure.NUnit;
using DotsAndBoxes.Game.Core.DTO;
using DotsAndBoxes.Game.Core.Enum;

namespace DotsAndBoxes.Game.Core.Test
{
    [AllureNUnit]
    public class GameEventTests
    {
        [Test]
        public void GameEvent_CanBeCreatedWithRequiredProperties()
        {
            var gameEvent = new GameEvent
            {
                type = GameEventType.EdgePlaced,
                player = "Player1",
                edgeId = "edge_1_2",
                boxX = "0",
                boxY = "1",
                nextPlayer = "Player2"
            };

            Assert.That(gameEvent.type, Is.EqualTo(GameEventType.EdgePlaced));
            Assert.That(gameEvent.player, Is.EqualTo("Player1"));
            Assert.That(gameEvent.edgeId, Is.EqualTo("edge_1_2"));
            Assert.That(gameEvent.boxX, Is.EqualTo("0"));
            Assert.That(gameEvent.boxY, Is.EqualTo("1"));
            Assert.That(gameEvent.nextPlayer, Is.EqualTo("Player2"));
        }

        [Test]
        public void GameEvent_RecordsWithSameValues_AreEqual()
        {
            var event1 = new GameEvent
            {
                type = GameEventType.BoxClaimed,
                player = "Player1",
                edgeId = "edge_1",
                boxX = "2",
                boxY = "3",
                nextPlayer = "Player1"
            };

            var event2 = new GameEvent
            {
                type = GameEventType.BoxClaimed,
                player = "Player1",
                edgeId = "edge_1",
                boxX = "2",
                boxY = "3",
                nextPlayer = "Player1"
            };

            Assert.That(event1, Is.EqualTo(event2));
        }

        [Test]
        public void GameEvent_RecordsWithDifferentValues_AreNotEqual()
        {
            var event1 = new GameEvent
            {
                type = GameEventType.TurnChanged,
                player = "Player1",
                edgeId = "edge_1",
                boxX = "0",
                boxY = "0",
                nextPlayer = "Player2"
            };

            var event2 = new GameEvent
            {
                type = GameEventType.GameOver,
                player = "Player1",
                edgeId = "edge_1",
                boxX = "0",
                boxY = "0",
                nextPlayer = "Player2"
            };

            Assert.That(event1, Is.Not.EqualTo(event2));
        }

        [Test]
        [TestCase(GameEventType.EdgePlaced)]
        [TestCase(GameEventType.BoxClaimed)]
        [TestCase(GameEventType.TurnChanged)]
        [TestCase(GameEventType.GameOver)]
        public void GameEvent_CanBeCreatedWithAnyEventType(GameEventType eventType)
        {
            var gameEvent = new GameEvent
            {
                type = eventType,
                player = "TestPlayer",
                edgeId = "test_edge",
                boxX = "0",
                boxY = "0",
                nextPlayer = "TestPlayer"
            };

            Assert.That(gameEvent.type, Is.EqualTo(eventType));
        }
    }
}