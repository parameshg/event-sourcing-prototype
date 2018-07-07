using Microsoft.EntityFrameworkCore;
using NinjaBoard.Database.Entities;

namespace NinjaBoard.Database
{
    public class BoardDbContext : DbContext
    {
        public DbSet<Game> Games { get; set; }

        public DbSet<Layout> Transactions { get; set; }

        public BoardDbContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder o)
        {
        }
    }

    // used only for ef migrations
    public class SqlServerBoardDbContext : BoardDbContext
    {
        public SqlServerBoardDbContext(DbContextOptions options)
            : base(options)
        {
        }
    }

    // used only for ef migrations
    public class SqliteBoardDbContext : BoardDbContext
    {
        public SqliteBoardDbContext(DbContextOptions options)
            : base(options)
        {
        }
    }
}