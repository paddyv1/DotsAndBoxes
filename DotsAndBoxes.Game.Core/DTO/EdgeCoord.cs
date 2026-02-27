using System;
using System.Collections.Generic;
using System.Text;
using DotsAndBoxes.Game.Core.Enum;

namespace DotsAndBoxes.Game.Core.DTO
{
    /// <summary>
    /// Record to transfer coords of edge piece in grid
    /// </summary>
    public record EdgeCoord
    {
        public required EdgeOrient orient { get; set; }
        public required int x { get; set; }
        public required int y { get; set; }
    
    }

}