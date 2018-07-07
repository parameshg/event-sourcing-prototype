using System;

namespace NinjaBoard.Database.Entities
{
    public class Layout
    {
        public Guid Id { get; set; }

        public Guid Game { get; set; }

        public int Coin { get; set; }

        public int Position { get; set; }
    }
}