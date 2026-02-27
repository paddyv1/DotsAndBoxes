using System;
using System.Collections.Generic;
using System.Text;

namespace DotsAndBoxes.Game.Core.Enum
{
    /// <summary>
    /// Key game events which can occur
    /// </summary>
    public enum GameEventType
    {
        EdgePlaced,
        BoxClaimed,
        TurnChanged,
        GameOver
    }
}
