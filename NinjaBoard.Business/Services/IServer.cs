using System;
using System.Collections.Generic;
using NinjaBoard.Events;
using NinjaBoard.Model;

namespace NinjaBoard.Business.Services
{
    public interface IServer
    {
        List<Guid> GetGames();

        Dictionary<Coin, Position> GetGameById(Guid gameId);

        List<Event> GetEvents(Guid gameId);

        Guid CreateGame();

        bool Move(Guid gameId, Player player, Coin coin, Position source, Position destination);

        bool Replace(Guid gameId, Player player, Coin coin, Coin target, Position source, Position destination);

        bool DeleteGame(Guid gameId);
    }
}