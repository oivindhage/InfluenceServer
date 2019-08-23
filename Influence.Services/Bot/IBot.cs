using Influence.Domain;

namespace Influence.Services.Bot
{
    public interface IBot
    {
        Tile Reinforce(Session session);
        MoveInstruction MoveAndAttack(Session session);
    }
}
