using System;

namespace NinjaBoard.Events
{
    public class GameEndedEvent : Event
    {
        public Guid GameId { get; set; }

        public DateTime Timestamp { get; set; }
    }
}