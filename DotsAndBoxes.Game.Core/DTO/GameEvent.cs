using System;
using System.Collections.Generic;
using System.Text;
using DotsAndBoxes.Game.Core.Enum;

namespace DotsAndBoxes.Game.Core.DTO
{

    /// <summary>
    /// Record to transfer game event info
    /// Need to update typing
    /// </summary>
    public record GameEvent
    {
        public required GameEventType type {  get; set; }
        public required string player { get; set; }
        public required string edgeId { get; set; }
        public required string boxX { get; set; }
        public required string boxY { get; set; }
        public required string nextPlayer { get; set; }

        public GameEvent()
        {
            
        }
    }
}
