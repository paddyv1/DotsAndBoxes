using System;
using System.Collections.Generic;
using DotsAndBoxes.Game.Core.DTO;
using DotsAndBoxes.Game.Core.Enum;
using DotsAndBoxes.Game.Core.Helper;

namespace DotsAndBoxes.Game.Core
{
    public class GameEngine
    {
        public sbyte[] _hEdgeOwners = CreateArrayWithValue(Board12x12.HEdgeCount, -1);
        public sbyte[] _vEdgeOwners = CreateArrayWithValue(Board12x12.VEdgeCount, -1);
        public sbyte[] _boxOwners = CreateArrayWithValue(Board12x12.BoxCount, -1);

        public int currentPlayer { get; private set; } = 0;
        public bool gameOver { get; private set; } = false;
        public long stateVersion { get; private set; } = 0;
        public int[] scores { get; } = new int[2];
        public List<GameEvent> gameEvents { get; private set; } = new List<GameEvent>();

        public GameEngine() { }

        public int HIndex(int x, int y) => y * Board12x12.W + x;
        public int VIndex(int x, int y) => y * (Board12x12.W + 1) + x;
        public int boxIndex(int x, int y) => y * Board12x12.W + x;

        private static sbyte[] CreateArrayWithValue(int length, sbyte value)
        {
            var arr = new sbyte[length];
            for (var i = 0; i < length; i++)
                arr[i] = value;
            return arr;
        }

        public int GetEdgeOwner(int edgeId)
        {
            if (!EdgeHelper.TryDecode(edgeId, out var coord) || coord is null)
                return -1;

            return coord.orient == EdgeOrient.Horizontal
                ? _hEdgeOwners[edgeId]
                : _vEdgeOwners[edgeId - Board12x12.HEdgeCount];
        }

        public void SetEdgeOwner(int edgeId, int player)
        {
            if (player != 0 && player != 1)
                throw new ArgumentException(nameof(player));

            if (!EdgeHelper.TryDecode(edgeId, out var coord) || coord is null)
                return;

            sbyte playerFormatted = (sbyte)player;
            if (coord.orient == EdgeOrient.Horizontal)
                _hEdgeOwners[edgeId] = playerFormatted;
            else
                _vEdgeOwners[edgeId - Board12x12.HEdgeCount] = playerFormatted;
        }

        public bool IsEdgeFree(int edgeId)
        {
            if (!EdgeHelper.TryDecode(edgeId, out var coord) || coord is null)
                return false;

            return coord.orient == EdgeOrient.Horizontal
                ? _hEdgeOwners[edgeId] == -1
                : _vEdgeOwners[edgeId - Board12x12.HEdgeCount] == -1;
        }

        public enum Direction { Above, Below, Left, Right }

        public bool IsBoxComplete(int boxX, int boxY)
        {
            if (boxX < 0 || boxX >= Board12x12.W || boxY < 0 || boxY >= Board12x12.H)
                return false;

            EdgeHelper.TryEncode(new EdgeCoord { orient = EdgeOrient.Horizontal, x = boxX, y = boxY }, out int hTop);
            EdgeHelper.TryEncode(new EdgeCoord { orient = EdgeOrient.Horizontal, x = boxX, y = boxY + 1 }, out int hBottom);
            EdgeHelper.TryEncode(new EdgeCoord { orient = EdgeOrient.Vertical, x = boxX, y = boxY }, out int vLeft);
            EdgeHelper.TryEncode(new EdgeCoord { orient = EdgeOrient.Vertical, x = boxX + 1, y = boxY }, out int vRight);

            return !IsEdgeFree(hTop) && !IsEdgeFree(hBottom) && !IsEdgeFree(vLeft) && !IsEdgeFree(vRight);
        }

        public ApplyMoveResult ApplyMove(int player, int edgeId)
        {
            if (gameOver)
            {
                return new ApplyMoveResult
                {
                    Ok = true,
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

            var claimedBoxesThisTurn = new List<(int boxX, int boxY)>();
            foreach (var (boxX, boxY) in EdgeHelper.GetAdjacentBoxCoords(edgeId))
            {
                int idx = boxIndex(boxX, boxY);
                if (IsBoxComplete(boxX, boxY) && _boxOwners[idx] == -1)
                    claimedBoxesThisTurn.Add((boxX, boxY));
            }

            bool boxClaimed = claimedBoxesThisTurn.Count > 0;
            foreach (var (boxX, boxY) in claimedBoxesThisTurn)
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

            if (!boxClaimed)
                currentPlayer = (currentPlayer + 1) % 2;

            stateVersion++;

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
