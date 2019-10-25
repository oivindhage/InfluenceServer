using Influence.Domain;

namespace Influence.Services.Bot
{
    public interface IBot
    {
        Tile GetTileToReinforce(Session session);
        MoveInstruction GetMoveOrAttackInstruction(Session session);
    }
}
