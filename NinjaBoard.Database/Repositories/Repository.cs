using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using NinjaBoard.Model;

namespace NinjaBoard.Database.Repositories
{
    public class Repository : IRepository
    {
        private BoardDbContext db;

        public Repository()
        {
            var builder = new DbContextOptionsBuilder<BoardDbContext>();

            builder.UseInMemoryDatabase("chessboard");

            db = new BoardDbContext(builder.Options);
        }

        public List<Guid> GetGames()
        {
            var result = new List<Guid>();

            foreach (var i in db.Games)
                result.Add(i.Id);

            return result;
        }

        public Guid CreateGame()
        {
            var result = Guid.Empty;

            var id = Guid.NewGuid();

            db.Games.Add(new Entities.Game()
            {
                Id = id,
                Started = DateTime.Now
            });

            if (db.SaveChanges() > 0)
                result = id;

            return result;
        }

        public bool DeleteGame(Guid id)
        {
            var result = false;

            var entity = db.Games.FirstOrDefault(i => i.Id.Equals(id));

            if (entity != null)
            {
                db.Games.Remove(entity);
                result = db.SaveChanges() > 0;
            }

            return result;
        }

        public Dictionary<Coin, Position> GetPositions(Guid game)
        {
            var result = new Dictionary<Coin, Position>();

            foreach (var i in db.Transactions.Where(i => i.Game.Equals(game)).ToArray())
                result.Add((Coin)i.Coin, (Position)i.Position);

            return result;
        }

        public Guid CreatePosition(Guid game, Coin coin, Position position)
        {
            var result = Guid.Empty;

            var id = Guid.NewGuid();

            db.Transactions.Add(new Entities.Layout()
            {
                Id = id,
                Game = game,
                Coin = (int)coin,
                Position = (int)position
            });

            if (db.SaveChanges() > 0)
                result = id;

            return result;
        }

        public bool SetPosition(Guid game, Coin coin, Position position)
        {
            var result = false;

            var entity = db.Transactions.FirstOrDefault(i => i.Game.Equals(game) && i.Coin.Equals((int)coin));

            if (entity != null)
            {
                entity.Coin = (int)coin;
                entity.Position = (int)position;
            }

            result = db.SaveChanges() > 0;

            return result;
        }

        public bool DeletePositions(Guid game)
        {
            var result = true;

            foreach (var i in db.Transactions.Where(i => i.Game.Equals(game)).ToArray())
            {
                db.Transactions.Remove(i);
                result = result && db.SaveChanges() > 0;
            }

            return result;
        }
    }
}