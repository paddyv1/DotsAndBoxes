using System;
using Allure.NUnit;
using DotsAndBoxes.Game.Core;
using DotsAndBoxes.Game.Core.DTO;
using DotsAndBoxes.Game.Core.Enum;
using NUnit.Framework;
using NUnit.Framework.Legacy;

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
            int player = 0;

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


        [Test]
        public void ApplyMove_ValidEdge_SwitchesPlayer()
        {
            var gameEngine = new GameEngine();

            //checl edge 13 is free
            var result = gameEngine.IsEdgeFree(13);
            Assert.That(result, Is.EqualTo(true));

            //player 0 apply to claim
            //check edge 13 is claimed by 0
            gameEngine.ApplyMove(0, 13);
            Assert.That(gameEngine._hEdgeOwners[13], Is.EqualTo((sbyte)0));
            //check current player
            Assert.That(gameEngine.currentPlayer, Is.EqualTo(1));

            //player 1 apply apply edge 169,
            gameEngine.ApplyMove(1, 169);
            Assert.That(gameEngine._vEdgeOwners[13], Is.EqualTo((sbyte)1));
            Assert.That(gameEngine.currentPlayer, Is.EqualTo(0));

            //player 0 claim edge 25
            gameEngine.ApplyMove(0, 25);
            Assert.That(gameEngine._hEdgeOwners[13], Is.EqualTo((sbyte)0));
            //check current player
            Assert.That(gameEngine.currentPlayer, Is.EqualTo(1));


            //player 1 apply apply edge 169,
            ApplyMoveResult boxClaimedMove = gameEngine.ApplyMove(1, 181);
            Assert.That(gameEngine._vEdgeOwners[25], Is.EqualTo((sbyte)1));
            Assert.That(gameEngine.currentPlayer, Is.EqualTo(1));
            Assert.That(boxClaimedMove.Ok, Is.True);
            Assert.That(boxClaimedMove.error, Is.EqualTo(MoveError.None));








        }



    }
}
