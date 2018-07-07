using System;
using System.Collections.Generic;
using NinjaBoard.Database.Repositories;
using NinjaBoard.Model;

namespace NinjaBoard.Business.Services
{
    public class Server : IServer
    {
        private IRepository Repository { get; }

        public Server(IRepository repository)
        {
            Repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public List<Guid> GetGames()
        {
            var result = new List<Guid>();

            result.AddRange(Repository.GetGames());

            return result;
        }

        public Dictionary<Coin, Position> GetGameById(Guid id)
        {
            return new Dictionary<Coin, Position>(Repository.GetPositions(id));
        }

        public Guid CreateGame()
        {
            var result = Guid.Empty;

            result = Repository.CreateGame();

            Repository.CreatePosition(result, Coin.Black_Rook_1, Position.A8);
            Repository.CreatePosition(result, Coin.Black_Knight_1, Position.B8);
            Repository.CreatePosition(result, Coin.Black_Bishop_1, Position.C8);
            Repository.CreatePosition(result, Coin.Black_Queen, Position.D8);
            Repository.CreatePosition(result, Coin.Black_King, Position.E8);
            Repository.CreatePosition(result, Coin.Black_Bishop_2, Position.F8);
            Repository.CreatePosition(result, Coin.Black_Knight_2, Position.G8);
            Repository.CreatePosition(result, Coin.Black_Rook_2, Position.H8);

            Repository.CreatePosition(result, Coin.Black_Pawn_1, Position.A7);
            Repository.CreatePosition(result, Coin.Black_Pawn_2, Position.B7);
            Repository.CreatePosition(result, Coin.Black_Pawn_3, Position.C7);
            Repository.CreatePosition(result, Coin.Black_Pawn_4, Position.D7);
            Repository.CreatePosition(result, Coin.Black_Pawn_5, Position.E7);
            Repository.CreatePosition(result, Coin.Black_Pawn_6, Position.F7);
            Repository.CreatePosition(result, Coin.Black_Pawn_7, Position.G7);
            Repository.CreatePosition(result, Coin.Black_Pawn_8, Position.H7);

            Repository.CreatePosition(result, Coin.White_Rook_2, Position.A1);
            Repository.CreatePosition(result, Coin.White_Knight_2, Position.B1);
            Repository.CreatePosition(result, Coin.White_Bishop_2, Position.C1);
            Repository.CreatePosition(result, Coin.White_King, Position.D1);
            Repository.CreatePosition(result, Coin.White_Queen, Position.E1);
            Repository.CreatePosition(result, Coin.White_Bishop_1, Position.F1);
            Repository.CreatePosition(result, Coin.White_Knight_1, Position.G1);
            Repository.CreatePosition(result, Coin.White_Rook_1, Position.H1);

            Repository.CreatePosition(result, Coin.White_Pawn_8, Position.A2);
            Repository.CreatePosition(result, Coin.White_Pawn_7, Position.B2);
            Repository.CreatePosition(result, Coin.White_Pawn_6, Position.C2);
            Repository.CreatePosition(result, Coin.White_Pawn_5, Position.D2);
            Repository.CreatePosition(result, Coin.White_Pawn_4, Position.E2);
            Repository.CreatePosition(result, Coin.White_Pawn_3, Position.F2);
            Repository.CreatePosition(result, Coin.White_Pawn_2, Position.G2);
            Repository.CreatePosition(result, Coin.White_Pawn_1, Position.H2);

            return result;
        }

        public bool UpdateGame(Guid game, Coin coin, Position position)
        {
            var result = false;

            result = Repository.SetPosition(game, coin, position);

            return result;
        }

        public bool DeleteGame(Guid id)
        {
            var result = false;

            result = Repository.DeleteGame(id) && Repository.DeletePositions(id);

            return result;
        }
    }
}