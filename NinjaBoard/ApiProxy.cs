using NinjaBoard.Model;

namespace NinjaBoard
{
    public class ApiProxy
    {
        public bool CanMove(Player player, Person person, Position source, Position destination)
        {
            return true;
        }

        public bool CanReplace(Player player, Person person, Person target)
        {
            return true;
        }
    }
}