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
            //first 0->155 horizontal,
            //156->311 vertical
            EdgeOrient calculatedOreint = edgeId < 156 ? EdgeOrient.Horizontal : EdgeOrient.Vertical;

            int calculatedX = edgeId % 12;
            int calculatedY = edgeId / 12;

            coord = new EdgeCoord
            {
                orient = calculatedOreint,
                x = calculatedX,
                y = calculatedY
            };

            return true;

        }

        public static bool TryEncode(EdgeCoord coord, out int edgeId)
        {
            edgeId = -1;

            if (coord.orient == EdgeOrient.Horizontal)
            {
                edgeId = coord.y * 12 + coord.x;
            }
            else if (coord.orient == EdgeOrient.Vertical)
            {
                edgeId = 156 + (coord.y * 12 + coord.x);
            }
            else
            {
                return false;
            }

            // Optionally, validate the edgeId
            return edgeId >= 0 && edgeId <= 311;
        }

        public static bool IsValidEdgeId(int edgeId)
        {
            if(edgeId > 311 && edgeId > 0) { return  false; }

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
