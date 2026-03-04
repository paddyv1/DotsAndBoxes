using DotsAndBoxes.Shared.DTOs.EventsResponse;
using DotsAndBoxes.Shared.DTOs.Requests;
using Microsoft.AspNetCore.SignalR;

namespace DotsAndBoxes.GameHub
{
    public class GameHub : Hub
    {
        private readonly RoomManager _roomManager;

        public GameHub(RoomManager roomManager)
        {
            _roomManager = roomManager;
        }

        public async Task FindMatch(FindMatchRequest request)
        {
            string connectionId = Context.ConnectionId;
            var (room, playerIndex) = _roomManager.EnqueueOrMatch(connectionId);

            if (room == null)
                return;

            await Groups.AddToGroupAsync(connectionId, $"room:{room.RoomId}");
            foreach (var otherConn in room.PlayerIndices.Keys.Where(id => id != connectionId))
                await Groups.AddToGroupAsync(otherConn, $"room:{room.RoomId}");

            foreach (var kvp in room.PlayerIndices)
            {
                var dto = new MatchFoundDto(room.RoomId, kvp.Value, room.PlayerIndices.Count);
                await Clients.Client(kvp.Key).SendAsync("MatchFound", dto);

                var snapshot = room.Engine.CreateSnapshot(room.RoomId);
                await Clients.Client(kvp.Key).SendAsync("Snapshot", snapshot);
            }
        }

        public async Task MakeMove(MakeMoveRequest request)
        {
            string connectionId = Context.ConnectionId;
            var room = _roomManager.GetRoom(request.RoomId);
            if (room == null || !room.PlayerIndices.ContainsKey(connectionId))
            {
                await Clients.Caller.SendAsync("MoveRejected", new MoveRejectedDto(request.RoomId, "Room not found or not authorized", -1));
                return;
            }

            int playerIndex = room.PlayerIndices[connectionId];
            await room.Lock.WaitAsync();
            try
            {
                var result = room.Engine.ApplyMove(playerIndex, request.EdgeId);
                if (!result.Ok)
                {
                    await Clients.Caller.SendAsync("MoveRejected", new MoveRejectedDto(request.RoomId, result.error.ToString(), result.StateVersion));
                    return;
                }

                var events = result.Events.Select(e => e.ToDto()).ToArray();
                var eventBatch = new EventBatchDto(request.RoomId, result.StateVersion, events);
                await Clients.Group($"room:{request.RoomId}").SendAsync("Events", eventBatch);
            }
            finally
            {
                room.Lock.Release();
            }
        }

        public async Task GetSnapshot(GetSnapshotRequest request)
        {
            var room = _roomManager.GetRoom(request.RoomId);
            if (room == null)
            {
                await Clients.Caller.SendAsync("Snapshot", null);
                return;
            }
            var snapshot = room.Engine.CreateSnapshot(room.RoomId);
            await Clients.Caller.SendAsync("Snapshot", snapshot);
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            string connectionId = Context.ConnectionId;
            _roomManager.TryRemoveWaiting(connectionId);

            var room = _roomManager.GetRoomByConnectionId(connectionId);
            if (room != null)
            {
                _roomManager.RemoveRoom(room.RoomId);
                var opponent = room.PlayerIndices.Keys.FirstOrDefault(id => id != connectionId);
                if (opponent != null)
                    await Clients.Client(opponent).SendAsync("OpponentDisconnected");
            }

            await base.OnDisconnectedAsync(exception);
        }
    }
}
