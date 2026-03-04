using System;
using System.Collections.Generic;
using System.Text;

namespace DotsAndBoxes.Shared.DTOs.EventsResponse
{
    public abstract record GameEventDto;

    public record EdgePlacedEventDto(int Player, int EdgeId) : GameEventDto;
    public record BoxClaimedEventDto(int Player, int BoxX, int BoxY) : GameEventDto;
    public record TurnChangedEventDto(int NextPlayer) : GameEventDto;
    public record GameOverEventDto() : GameEventDto;
}
