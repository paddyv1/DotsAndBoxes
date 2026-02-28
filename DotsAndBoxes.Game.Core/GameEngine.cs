using System;
using System.Collections.Generic;
using System.Text;

namespace DotsAndBoxes.Game.Core
{
    public class GameEngine
    {
        public sbyte[] _hEdgeOwners = CreateArrayWithValue(156, -1);
        public sbyte[] _vEdgeOwners = CreateArrayWithValue(156, -1);


        public int currentPlayer { get; private set; } = 0;
        public bool gameOver { get; private set; } = false;
        public long stateVersion { get; private set; } = 0;
        public int[] scores { get; } = new int[2];


        public GameEngine()
        {
            
        }

        private static sbyte[] CreateArrayWithValue(int length, sbyte value)
        {
            var arr = new sbyte[length];
            for (var i = 0; i < length; i++)
            {
                arr[i] = value;
            }
            return arr;
        }

        public sbyte GetEdgeOwner(int edgeId)
        {
            if(edgeId < 156)
            {
                return _hEdgeOwners[edgeId];
            }
            else
            {
                edgeId = edgeId - 156;
                return _vEdgeOwners[edgeId];
            }
        }

        public void SetEdgeOwner(int edgeId, sbyte player)
        {
            sbyte playerName1= 0, player2 = 1;


            if(player != playerName1 || player2 != 1)
            {
                throw new ArgumentException();
            }

            if(edgeId > 155)
            {
                edgeId = edgeId - 156;
                _vEdgeOwners[edgeId] = player;
            }
            else
            {
                _hEdgeOwners[edgeId] = player;
            }

        }

        public bool IsEdgeFree(int edgeId)
        {
            if (edgeId < 156)
            {
                if (_hEdgeOwners[edgeId] == -1)
                {
                    return true;
                }
            }
            else
            {
                edgeId = edgeId - 156;
                if (_vEdgeOwners[edgeId] == -1)
                {
                    return true;
                }
            }


            return false;
        }
    }
}
