using System;
using System.Collections.Generic;
using System.Text;
using DotsAndBoxes.Game.Core.DTO;
using DotsAndBoxes.Game.Core.Enum;

namespace DotsAndBoxes.Game.Core.Helper
{
    public static class EdgeHelper
    {
        public static bool TryDecode(int edgeId, out EdgeCoord? coord)
        {
            EdgeOrient calculatedOrient = edgeId < Board12x12.HEdgeCount ? EdgeOrient.Horizontal : EdgeOrient.Vertical;
            int baseId = calculatedOrient == EdgeOrient.Horizontal ? edgeId : edgeId - Board12x12.HEdgeCount;

            int calculatedX, calculatedY;
            if (calculatedOrient == EdgeOrient.Horizontal)
            {   
                calculatedX = baseId % Board12x12.W;  // column (0..W-1)
                calculatedY = baseId / Board12x12.W;  // row    (0..H)
            }
            else
            {
                calculatedX = baseId / Board12x12.H;  // column (0..W)
                calculatedY = baseId % Board12x12.H;  // row    (0..H-1)
            }

            coord = new EdgeCoord
            {
                orient = calculatedOrient,
                x = calculatedX,
                y = calculatedY
            };

            return true;
        }


        public static IEnumerable<(int boxX, int boxY)> GetAdjacentBoxCoords(int edgeId)
        {
            var boxes = new List<(int, int)>();
            // Horizontal edges
            if (edgeId < Board12x12.HEdgeCount)
            {
                int h = edgeId % Board12x12.W;
                int v = edgeId / Board12x12.W;
                // Top box (if not on top row)
                if (v > 0)
                    boxes.Add((h, v - 1));
                // Bottom box (if not on bottom row)
                if (v < Board12x12.H)
                    boxes.Add((h, v));
            }
            // Vertical edges
            else
            {
                int id = edgeId - Board12x12.HEdgeCount;
                int col = id / Board12x12.H;  // column (0..W)
                int row = id % Board12x12.H;  // row    (0..H-1)
                // Left box (if not on leftmost column)
                if (col > 0)
                    boxes.Add((col - 1, row));
                // Right box (if not on rightmost column)
                if (col < Board12x12.W)
                    boxes.Add((col, row));
            }
            return boxes;
        }


        public static bool TryEncode(EdgeCoord coord, out int edgeId)
        {
            edgeId = -1;

            if (coord.orient == EdgeOrient.Horizontal)
            {
                edgeId = coord.y * Board12x12.W + coord.x;
            }
            else if (coord.orient == EdgeOrient.Vertical)
            {
                edgeId = Board12x12.HEdgeCount + (coord.x * Board12x12.H + coord.y);
            }
            else
            {
                return false;
            }

            return edgeId >= 0 && edgeId <= Board12x12.EdgeCount - 1;
        }

        public static bool IsValidEdgeId(int edgeId)
        {
            if(edgeId > 311 || edgeId < 0) { return  false; }

            return true;
        }

        public static bool IsValidCoord(EdgeCoord coord)
        {
            if(coord is null) {  return false; }

            if (coord.y < 0 || coord.y > 12) {  return false; }
            
            if (coord.x <  0 || coord.x > 12) { return false;  }

            return true;
        }
    }
}
