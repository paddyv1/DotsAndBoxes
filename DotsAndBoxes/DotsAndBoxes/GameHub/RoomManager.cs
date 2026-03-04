using DotsAndBoxes.Game.Core;

namespace DotsAndBoxes.GameHub
{
    public class RoomManager
    {
        // One waiting player slot (simple matchmaking)
        private string? _waitingConnectionId;
        private object _lock = new();

        // Map RoomId → Room
        private Dictionary<string, Room> _rooms = new();

        public (Room? room, int yourPlayerIndex) EnqueueOrMatch(string connectionId)
        {
            lock (_lock)
            {
                if (_waitingConnectionId == null)
                {
                    _waitingConnectionId = connectionId;
                    return (null, -1); // Wait
                }
                else if (_waitingConnectionId == connectionId)
                {
                    return (null, -1); // Don't match self
                }
                else
                {
                    // Create room
                    var roomId = Guid.NewGuid().ToString("N");
                    var room = new Room(roomId, _waitingConnectionId, connectionId);
                    _rooms.Add(roomId, room);
                    _waitingConnectionId = null;
                    return (room, 1); // You are player 1 (waiting was 0)
                }
            }
        }

        public Room? GetRoom(string roomId)
        {
            lock (_lock) { return _rooms.TryGetValue(roomId, out var r) ? r : null; }
        }

        // Other helpers: RemoveRoom, CleanupOnDisconnect, etc.
    }

    public class Room
    {
        public string RoomId { get; }
        public Dictionary<string, int> PlayerIndices { get; } // connId → 0 or 1
        public GameEngine Engine { get; }
        public SemaphoreSlim Lock { get; } = new(1, 1);

        // constructor wires up the rest
        public Room(string roomId, string connectionA, string connectionB)
        {
            RoomId = roomId;
            Engine = new GameEngine();
            PlayerIndices = new Dictionary<string, int>
            {
                [connectionA] = 0,
                [connectionB] = 1
            };
        }
    }
}
