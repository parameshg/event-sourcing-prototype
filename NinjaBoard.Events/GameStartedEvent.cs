using System;

namespace NinjaBoard.Events
{
    public class GameStartedEvent : Event
    {
        public Guid GameId { get; set; }

        public DateTime Timestamp { get; set; }
    }
}