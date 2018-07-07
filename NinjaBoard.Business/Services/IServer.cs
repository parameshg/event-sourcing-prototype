using System;
using System.Collections.Generic;
using NinjaBoard.Model;

namespace NinjaBoard.Business.Services
{
    public interface IServer
    {
        List<Guid> GetGames();

        Dictionary<Coin, Position> GetGameById(Guid id);

        Guid CreateGame();

        bool UpdateGame(Guid id, Coin coin, Position position);

        bool DeleteGame(Guid id);
    }
}