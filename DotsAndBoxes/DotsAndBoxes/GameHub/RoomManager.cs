using DotsAndBoxes.Game.Core;

namespace DotsAndBoxes.GameHub
{
    public class RoomManager
    {
        private string? _waitingConnectionId = null;
        private readonly object _lock = new();
        private readonly Dictionary<string, Room> _rooms = new();

        public (Room? room, int yourPlayerIndex) EnqueueOrMatch(string connectionId)
        {
            lock (_lock)
            {
                if (_waitingConnectionId == null)
                {
                    _waitingConnectionId = connectionId;
                    return (null, -1);
                }
                else if (_waitingConnectionId == connectionId)
                {
                    return (null, -1);
                }
                else
                {
                    var roomId = Guid.NewGuid().ToString("N");
                    var room = new Room(roomId, _waitingConnectionId, connectionId);
                    _rooms.Add(roomId, room);
                    _waitingConnectionId = null;
                    return (room, 1);
                }
            }
        }

        public Room? GetRoom(string roomId)
        {
            lock (_lock)
            {
                return _rooms.TryGetValue(roomId, out var room) ? room : null;
            }
        }

        public Room? GetRoomByConnectionId(string connectionId)
        {
            lock (_lock)
            {
                return _rooms.Values.FirstOrDefault(r => r.PlayerIndices.ContainsKey(connectionId));
            }
        }

        public void TryRemoveWaiting(string connectionId)
        {
            lock (_lock)
            {
                if (_waitingConnectionId == connectionId)
                    _waitingConnectionId = null;
            }
        }

        public void RemoveRoom(string roomId)
        {
            lock (_lock)
            {
                _rooms.Remove(roomId);
            }
        }
    }

    public class Room
    {
        public string RoomId { get; }
        public Dictionary<string, int> PlayerIndices { get; }
        public GameEngine Engine { get; }
        public SemaphoreSlim Lock { get; } = new SemaphoreSlim(1, 1);

        public Room(string roomId, string connA, string connB)
        {
            RoomId = roomId;
            Engine = new GameEngine();
            PlayerIndices = new Dictionary<string, int>
            {
                [connA] = 0,
                [connB] = 1
            };
        }
    }
}
