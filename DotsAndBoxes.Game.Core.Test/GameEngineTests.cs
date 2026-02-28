using Allure.NUnit;
using DotsAndBoxes.Game.Core;
using NUnit.Framework;
using System;

namespace DotsAndBoxes.Game.Core.Test
{
    [TestFixture]
    [AllureNUnit]
    public class GameEngineTests
    {
        [Test]
        public void GetEdgeOwner_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var gameEngine = new GameEngine();
            int edgeId = 0;

            // Act
            var result = gameEngine.GetEdgeOwner(
                edgeId);

            // Assert
            Assert.That(result, Is.EqualTo(-1));
        }

        [Test]
        public void SetEdgeOwner_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var gameEngine = new GameEngine();
            int edgeId = 0;
            sbyte player = 0;

            // Act
            gameEngine.SetEdgeOwner(
                edgeId,
                player);

            var result = gameEngine.GetEdgeOwner(edgeId);

            // Assert
            Assert.That(result, Is.EqualTo(player));
        }

        [Test]
        public void IsEdgeFree_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var gameEngine = new GameEngine();
            int edgeId = 0;

            // Act
            var result = gameEngine.IsEdgeFree(
                edgeId);

            // Assert
            Assert.That(result, Is.EqualTo(true));
        }


        
    }
}
