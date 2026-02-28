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
            //first check for gameover
            if(gameOver == true)
            {
                ApplyMoveResult gameOverResult = new ApplyMoveResult
                {
                    Ok = false,
                    error = MoveError.GameOver,
                    StateVersion = stateVersion,
                    Events = gameEvents
                };

                return gameOverResult;
            }

            //if player is not current player
            if(player != currentPlayer)
            {
                ApplyMoveResult incorrectPlayerResult = new ApplyMoveResult
                {
                    Ok = false,
                    error = MoveError.WrongTurn,
                    StateVersion = stateVersion,
                    Events = gameEvents
                };

                return incorrectPlayerResult;
            }


            //out of range move
            if (!EdgeHelper.IsValidEdgeId(edgeId))
            {
                ApplyMoveResult incorrectPlayerResult = new ApplyMoveResult
                {
                    Ok = false,
                    error = MoveError.OutOfRange,
                    StateVersion = stateVersion,
                    Events = gameEvents
                };

                return incorrectPlayerResult;
            }

            //edge already owned
            if (!IsEdgeFree(edgeId))
            {
                ApplyMoveResult edgeNotFreeResult = new ApplyMoveResult
                {
                    Ok = false,
                    error = MoveError.EdgeAlreadyTaken,
                    StateVersion = stateVersion,
                    Events = gameEvents
                };

                return edgeNotFreeResult;
            }



            //at this point in method move will be valid, apply logic to
            //apply the move
            SetEdgeOwner(edgeId, player);
            int nextPlayer = (currentPlayer+1)% 2;
            //add to gameevent list
            gameEvents.Add(new GameEvent
            {
                type = GameEventType.EdgePlaced,
                edgeId = edgeId,
                player = player,
                nextPlayer = nextPlayer,
                boxX = 0,
                boxY = 0

            });

            //change player in game engine
            currentPlayer = nextPlayer;
            //increment state version
            stateVersion++;



            ApplyMoveResult ValidResult = new ApplyMoveResult
            {
                Ok = true,
                error = MoveError.None,
                StateVersion = stateVersion,
                Events = gameEvents
            };

            return ValidResult;

        }

        public void Reset()
        {
            throw new NotImplementedException("Can Add Later");
        }
    }

    
}
