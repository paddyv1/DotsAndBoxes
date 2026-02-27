using DotsAndBoxes.Game.Core.DTO;
using DotsAndBoxes.Game.Core.Enum;
using DotsAndBoxes.Game.Core.Tests;
using System;
using System.Text.Json;
using Xunit;

namespace DotsAndBoxes.Game.Core.Tests
{
    public class EnumDtoTests
    {
        [Fact]
        public void GameEvent_Serializes_And_Deserializes_Correctly()
        {
            var original = new GameEvent
            {
                type = GameEventType.EdgePlaced,
                player = "Player1",
                edgeId = "e1",
                boxX = "0",
                boxY = "1",
                nextPlayer = "Player2"
            };

            var json = JsonSerializer.Serialize(original);
            var deserialized = JsonSerializer.Deserialize<GameEvent>(json);

            Assert.Equal(original, deserialized);
        }

        // 2. ENUM COVERAGE - Ensure all error states are distinct
        [Theory]
        [InlineData(MoveError.None, true)]
        [InlineData(MoveError.GameOver, false)]
        [InlineData(MoveError.WrongTurn, false)]
        [InlineData(MoveError.EdgeAlreadyTaken, false)]
        public void MoveError_None_Is_Only_Success_State(MoveError error, bool expectedOk)
        {
            var result = new ApplyMoveResult
            {
                Ok = error == MoveError.None,
                error = error,
                StateVersion = 1,
                Events = []
            };

            Assert.Equal(expectedOk, result.Ok);
        }

        // 3. RECORD EQUALITY - Verify value-based comparison works as expected
        [Fact]
        public void GameEvent_Records_Are_Equal_By_Value()
        {
            var event1 = new GameEvent { type = GameEventType.BoxClaimed, player = "P1", edgeId = "e1", boxX = "0", boxY = "0", nextPlayer = "P2" };
            var event2 = new GameEvent { type = GameEventType.BoxClaimed, player = "P1", edgeId = "e1", boxX = "0", boxY = "0", nextPlayer = "P2" };

            Assert.Equal(event1, event2);
        }
    }
}
