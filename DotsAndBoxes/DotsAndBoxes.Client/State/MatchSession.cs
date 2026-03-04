using DotsAndBoxes.Shared.DTOs.EventsResponse;

namespace DotsAndBoxes.Client.State;

/// <summary>
/// Scoped carrier that passes match details from the Lobby to the Game Room
/// without relying on query-string serialisation.
/// </summary>
public sealed class MatchSession
{
    public MatchFoundDto?  Match           { get; set; }
    public SnapshotDto?    InitialSnapshot { get; set; }
    public string          DisplayName     { get; set; } = "Player";
}