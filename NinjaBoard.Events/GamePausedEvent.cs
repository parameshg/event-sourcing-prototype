using System;

namespace NinjaBoard.Events
{
    public class GamePausedEvent : Event
    {
        public Guid GameId { get; set; }

        public DateTime Timestamp { get; set; }
    }
}