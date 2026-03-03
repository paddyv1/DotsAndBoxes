using System;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using System.Text;
using DotsAndBoxes.Game.Core.DTO;
using DotsAndBoxes.Game.Core.Enum;
using DotsAndBoxes.Game.Core.Helper;

namespace DotsAndBoxes.Game.Core
{
    public class GameEngine
    {
        public sbyte[] _hEdgeOwners = CreateArrayWithValue(Board12x12.HEdgeCount, -1);
        public sbyte[] _vEdgeOwners = CreateArrayWithValue(Board12x12.VEdgeCount, -1);

        //list to hold boxes in 12x12 gird
        public sbyte[] _boxOwners = CreateArrayWithValue(144, -1);


        public int currentPlayer { get; private set; } = 0;
        public bool gameOver { get; private set; } = false;
        public long stateVersion { get; private set; } = 0;
        public int[] scores { get; } = new int[2];

        public List<GameEvent> gameEvents { get; private set; } = new List<GameEvent>();


        public GameEngine()
        {
            
        }

        public int HIndex(int x, int y) => y * Board12x12.W + x;
        public int VIndex(int x, int y) => y * (Board12x12.W + 1) + x;

        public int boxIndex(int x, int y) => y * 12 + x;

        //need to implement still
        //look up 4 surrounding edges
        public bool IsBoxComplete(int boxX, int boxY)
        {
            // Bounds check (optional for safety)
            if (boxX < 0 || boxX >= Board12x12.W || boxY < 0 || boxY >= Board12x12.H)
                throw new ArgumentOutOfRangeException();

            // Calculate indices in each edge array
            int hTop = boxY * Board12x12.W + boxX;        // Horizontal top
            int hBottom = (boxY + 1) * Board12x12.W + boxX; // Horizontal bottom
            int vLeft = boxY * (Board12x12.W + 1) + boxX;   // Vertical left
            int vRight = boxY * (Board12x12.W + 1) + (boxX + 1); // Vertical right

            // If all four owners are not -1, the box is complete
            return
                _hEdgeOwners[hTop] != -1 &&
                _hEdgeOwners[hBottom] != -1 &&
                _vEdgeOwners[vLeft] != -1 &&
                _vEdgeOwners[vRight] != -1;
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

        public int GetEdgeOwner(int edgeId)
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

        public void SetEdgeOwner(int edgeId, int player)
        {



            if (player != 0 && player != 1)
            {
                throw new ArgumentException(nameof(player));
            }

            sbyte playerFormatted = ((sbyte)player);

            if(edgeId > 155)
            {
                edgeId = edgeId - 156;
                _vEdgeOwners[edgeId] = playerFormatted;
            }
            else
            {
                _hEdgeOwners[edgeId] = playerFormatted;
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


        public ApplyMoveResult ApplyMove(int player, int edgeId)
        {
            if (gameOver)
            {
                return new ApplyMoveResult
                {
                    Ok = false,
                    error = MoveError.GameOver,
                    StateVersion = stateVersion,
                    Events = gameEvents
                };
            }

            if (player != currentPlayer)
            {
                return new ApplyMoveResult
                {
                    Ok = false,
                    error = MoveError.WrongTurn,
                    StateVersion = stateVersion,
                    Events = gameEvents
                };
            }

            if (!EdgeHelper.IsValidEdgeId(edgeId))
            {
                return new ApplyMoveResult
                {
                    Ok = false,
                    error = MoveError.OutOfRange,
                    StateVersion = stateVersion,
                    Events = gameEvents
                };
            }

            if (!IsEdgeFree(edgeId))
            {
                return new ApplyMoveResult
                {
                    Ok = false,
                    error = MoveError.EdgeAlreadyTaken,
                    StateVersion = stateVersion,
                    Events = gameEvents
                };
            }

            SetEdgeOwner(edgeId, player);

            // Find affected boxes (up to 2)
            var claimedBoxes = new List<(int boxX, int boxY)>();
            if (edgeId < 156)
            {
                // Horizontal edge
                int h = edgeId % Board12x12.W;
                int v = edgeId / Board12x12.W;
                // Top box
                if (v > 0 && IsBoxComplete(h, v - 1) && _boxOwners[boxIndex(h, v - 1)] == -1)
                    claimedBoxes.Add((h, v - 1));
                // Bottom box
                if (v < Board12x12.H && IsBoxComplete(h, v) && _boxOwners[boxIndex(h, v)] == -1)
                    claimedBoxes.Add((h, v));
            }
            else
            {
                // Vertical edge
                int id = edgeId - 156;
                int h = id % (Board12x12.W + 1);
                int v = id / (Board12x12.W + 1);
                // Left box
                if (h > 0 && v < Board12x12.H && IsBoxComplete(h - 1, v) && _boxOwners[boxIndex(h - 1, v)] == -1)
                    claimedBoxes.Add((h - 1, v));
                // Right box
                if (h < Board12x12.W && v < Board12x12.H && IsBoxComplete(h, v) && _boxOwners[boxIndex(h, v)] == -1)
                    claimedBoxes.Add((h, v));
            }

            bool boxClaimed = claimedBoxes.Count > 0;
            foreach (var (boxX, boxY) in claimedBoxes)
            {
                int idx = boxIndex(boxX, boxY);
                _boxOwners[idx] = (sbyte)player;
                scores[player]++;
                gameEvents.Add(new GameEvent
                {
                    type = GameEventType.BoxClaimed,
                    edgeId = edgeId,
                    player = player,
                    nextPlayer = currentPlayer,
                    boxX = boxX,
                    boxY = boxY
                });
            }

            gameEvents.Add(new GameEvent
            {
                type = GameEventType.EdgePlaced,
                edgeId = edgeId,
                player = player,
                nextPlayer = boxClaimed ? currentPlayer : (currentPlayer + 1) % 2,
                boxX = 0,
                boxY = 0
            });

            // Only switch player if no box was claimed
            if (!boxClaimed)
                currentPlayer = (currentPlayer + 1) % 2;

            stateVersion++;

            // Check for game over
            if (!_boxOwners.Contains((sbyte)-1))
            {
                gameOver = true;
                gameEvents.Add(new GameEvent
                {
                    type = GameEventType.GameOver,
                    edgeId = edgeId,
                    player = player,
                    nextPlayer = currentPlayer,
                    boxX = 0,
                    boxY = 0
                });
            }

            return new ApplyMoveResult
            {
                Ok = true,
                error = gameOver ? MoveError.GameOver : MoveError.None,
                StateVersion = stateVersion,
                Events = gameEvents
            };
        }

        public void Reset()
        {
            throw new NotImplementedException("Can Add Later");
        }
    }

    
}
