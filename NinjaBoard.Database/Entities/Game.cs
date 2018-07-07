using System;

namespace NinjaBoard.Database.Entities
{
    public class Game
    {
        public Guid Id { get; set; }

        public DateTime Started { get; set; }
    }
}