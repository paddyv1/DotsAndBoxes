using DotsAndBoxes.Game.Core;
using DotsAndBoxes.Game.Core.DTO;
using DotsAndBoxes.Game.Core.Enum;
using DotsAndBoxes.Shared.DTOs.EventsResponse;

namespace DotsAndBoxes.GameHub
{
    internal static class GameEngineExtensions
    {
        public static SnapshotDto CreateSnapshot(this GameEngine engine, string roomId) =>
            new(roomId, engine.stateVersion, engine.currentPlayer, engine.scores, engine.gameOver,
                engine._hEdgeOwners, engine._vEdgeOwners, engine._boxOwners);

        public static GameEventDto ToDto(this GameEvent evt) => evt.type switch
        {
            GameEventType.EdgePlaced  => new EdgePlacedEventDto(evt.player, evt.edgeId),
            GameEventType.BoxClaimed  => new BoxClaimedEventDto(evt.player, evt.boxX, evt.boxY),
            GameEventType.TurnChanged => new TurnChangedEventDto(evt.nextPlayer),
            GameEventType.GameOver    => new GameOverEventDto(),
            _ => throw new InvalidOperationException($"Unknown event type: {evt.type}")
        };
    }
}