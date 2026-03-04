using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DotsAndBoxes.Shared.DTOs.EventsResponse
{
    [JsonPolymorphic(TypeDiscriminatorPropertyName = "$type")]
    [JsonDerivedType(typeof(EdgePlacedEventDto),  "edgePlaced")]
    [JsonDerivedType(typeof(BoxClaimedEventDto),  "boxClaimed")]
    [JsonDerivedType(typeof(TurnChangedEventDto), "turnChanged")]
    [JsonDerivedType(typeof(GameOverEventDto),    "gameOver")]
    public abstract record GameEventDto;

    public record EdgePlacedEventDto(int Player, int EdgeId) : GameEventDto;
    public record BoxClaimedEventDto(int Player, int BoxX, int BoxY) : GameEventDto;
    public record TurnChangedEventDto(int NextPlayer) : GameEventDto;
    public record GameOverEventDto() : GameEventDto;
}
