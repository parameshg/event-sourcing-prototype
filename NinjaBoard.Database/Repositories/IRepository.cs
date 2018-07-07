using System;
using System.Collections.Generic;
using NinjaBoard.Model;

namespace NinjaBoard.Database.Repositories
{
    public interface IRepository
    {
        List<Guid> GetGames();

        Guid CreateGame();

        bool DeleteGame(Guid id);

        Dictionary<Coin, Position> GetPositions(Guid game);

        Guid CreatePosition(Guid game, Coin coin, Position position);

        bool SetPosition(Guid game, Coin coin, Position position);

        bool DeletePositions(Guid game);
    }
}