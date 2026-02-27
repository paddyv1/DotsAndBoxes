using System;
using System.Collections.Generic;
using System.Text;

namespace DotsAndBoxes.Game.Core
{
    public static class Board12x12
    {
        public const int W = 12;
        public const int H = 12;
        public const int HEdgeCount = W * (H + 1); // 156
        public const int VEdgeCount = (W + 1) * H; // 156
        public const int EdgeCount = HEdgeCount + VEdgeCount; // 312
        public const int BoxCount = W * H; // 144
        public const int PlayerCount = 2;
    }
}
