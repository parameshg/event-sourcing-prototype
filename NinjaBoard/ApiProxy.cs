using NinjaBoard.Model;

namespace NinjaBoard
{
    public class ApiProxy
    {
        public bool CanMove(Player player, Coin person, Position source, Position destination)
        {
            return true;
        }

        public bool CanReplace(Player player, Coin person, Coin target)
        {
            return true;
        }
    }
}