using System;
using System.Collections.Generic;
using System.Text;
using DotsAndBoxes.Game.Core.Enum;

namespace DotsAndBoxes.Game.Core.DTO
{
    public record ApplyMoveResult
    {
        public required bool Ok { get; set; }
        public required MoveError error { get; set; }
        public required long StateVersion { get; set; }
        public required IReadOnlyList<GameEvent> Events {  get; set; }
    }
}
