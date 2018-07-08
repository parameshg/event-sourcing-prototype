using System;
using NinjaBoard.Model;

namespace NinjaBoard.Events
{
    public class CoinReplacedEvent : Event
    {
        public Guid GameId { get; set; }

        public DateTime Timestamp { get; set; }

        public Player Player { get; set; }

        public Coin Coin { get; set; }

        public Coin Target { get; set; }

        public Position Source { get; set; }

        public Position Destination { get; set; }
    }
}