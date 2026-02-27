using System;
using System.Collections.Generic;
using System.Text;

namespace DotsAndBoxes.Game.Core.Enum
{
    /// <summary>
    /// Potential move erros which can occur in game
    /// </summary>
    public enum MoveError
    {
        None,
        GameOver,
        WrongTurn,
        OutOfRange,
        EdgeAlreadyTaken
    }
}
