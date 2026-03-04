using System;
using System.Collections.Generic;
using System.Text;

namespace DotsAndBoxes.Shared.DTOs.EventsResponse
{
    public record SnapshotDto(
    string RoomId,
    long StateVersion,
    int CurrentPlayer,
    int[] Scores,
    bool GameOver,
    sbyte[] HEdgeOwners,
    sbyte[] VEdgeOwners,
    sbyte[] BoxOwners
);
}
