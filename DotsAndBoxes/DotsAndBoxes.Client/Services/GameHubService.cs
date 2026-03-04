using DotsAndBoxes.Shared.DTOs.EventsResponse;
using DotsAndBoxes.Shared.DTOs.Requests;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;

namespace DotsAndBoxes.Client.Services;

/// <summary>
/// Wraps the SignalR HubConnection for the game hub.
/// Registered as Scoped — one connection per browser session.
/// </summary>
public sealed class GameHubService : IAsyncDisposable
{
    private readonly HubConnection _hub;

    // ── Server → Client ───────────────────────────────────────────────────────
    public event Action<MatchFoundDto>?   OnMatchFound;
    public event Action<SnapshotDto?>?    OnSnapshot;
    public event Action<EventBatchDto>?   OnEventBatch;
    public event Action<MoveRejectedDto>? OnMoveRejected;
    public event Action?                  OnOpponentDisconnected;

    // ── Connection lifecycle ──────────────────────────────────────────────────
    public event Action<Exception?>?      OnReconnecting;
    public event Action<string?>?         OnReconnected;
    public event Action<Exception?>?      OnClosed;

    public HubConnectionState State => _hub.State;

    public GameHubService(NavigationManager navigation)
    {
        var hubUrl = navigation.ToAbsoluteUri("/hub/game");

        _hub = new HubConnectionBuilder()
            .WithUrl(hubUrl)
            .WithAutomaticReconnect()
            .Build();

        _hub.On<MatchFoundDto>  ("MatchFound",           dto => OnMatchFound?.Invoke(dto));
        _hub.On<SnapshotDto?>   ("Snapshot",             dto => OnSnapshot?.Invoke(dto));
        _hub.On<EventBatchDto>  ("Events",               dto => OnEventBatch?.Invoke(dto));
        _hub.On<MoveRejectedDto>("MoveRejected",         dto => OnMoveRejected?.Invoke(dto));
        _hub.On                 ("OpponentDisconnected", ()  => OnOpponentDisconnected?.Invoke());

        _hub.Reconnecting += ex => { OnReconnecting?.Invoke(ex); return Task.CompletedTask; };
        _hub.Reconnected  += id => { OnReconnected?.Invoke(id);  return Task.CompletedTask; };
        _hub.Closed       += ex => { OnClosed?.Invoke(ex);       return Task.CompletedTask; };
    }

    // ── Client → Server ───────────────────────────────────────────────────────

    /// <summary>Opens the WebSocket to the hub.</summary>
    public Task ConnectAsync(CancellationToken ct = default) =>
        _hub.StartAsync(ct);

    /// <summary>
    /// Gracefully closes the connection.
    /// The server's OnDisconnectedAsync removes the caller from the waiting queue.
    /// </summary>
    public Task DisconnectAsync(CancellationToken ct = default) =>
        _hub.StopAsync(ct);

    /// <summary>Enters the match-making queue.</summary>
    public Task FindMatchAsync(FindMatchRequest request, CancellationToken ct = default) =>
        _hub.SendAsync("FindMatch", request, ct);

    /// <summary>Submits a move for the given edge.</summary>
    public Task MakeMoveAsync(MakeMoveRequest request, CancellationToken ct = default) =>
        _hub.SendAsync("MakeMove", request, ct);

    /// <summary>Requests a full board snapshot (used on reconnection).</summary>
    public Task GetSnapshotAsync(GetSnapshotRequest request, CancellationToken ct = default) =>
        _hub.SendAsync("GetSnapshot", request, ct);

    public ValueTask DisposeAsync() => _hub.DisposeAsync();
}