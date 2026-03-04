using System;
using System.Collections.Generic;
using System.Text;

namespace DotsAndBoxes.Shared.DTOs.EventsResponse
{
    public record EventBatchDto(string RoomId, long StateVersion, GameEventDto[] Events);
}
