using System;
using System.Collections.Generic;
using System.Text;

namespace DotsAndBoxes.Shared.DTOs.Requests
{
    public record FindMatchRequest(string? DisplayName = null);
}
