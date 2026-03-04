using System;
using System.Collections.Generic;
using System.Text;

namespace DotsAndBoxes.Shared.DTOs.EventsResponse
{
    public record MoveRejectedDto(string RoomId, string Reason, long StateVersion);
}
