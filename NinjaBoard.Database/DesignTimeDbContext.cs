using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace NinjaBoard.Database
{
    public class DesignTimeSqlServerDbContext : IDesignTimeDbContextFactory<SqlServerBoardDbContext>
    {
        public SqlServerBoardDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<SqlServerBoardDbContext>();

            builder.UseSqlServer("Server=localhost;Database=chessboard;User Id=sa;Password=sa;");

            return new SqlServerBoardDbContext(builder.Options);
        }
    }

    public class DesignTimeSqliteDbContextFactory : IDesignTimeDbContextFactory<SqliteBoardDbContext>
    {
        public SqliteBoardDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<SqliteBoardDbContext>();

            builder.UseSqlite("Data Source=.\\chessboard.db;");

            return new SqliteBoardDbContext(builder.Options);
        }
    }
}