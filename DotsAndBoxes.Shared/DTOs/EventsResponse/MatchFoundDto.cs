using System;
using System.Collections.Generic;
using System.Text;

namespace DotsAndBoxes.Shared.DTOs.EventsResponse
{
    public record MatchFoundDto(string RoomId, int YouArePlayerIndex, int PlayerCount);
}
