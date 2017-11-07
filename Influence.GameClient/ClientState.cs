using Influence.Domain;
using static Influence.Domain.Consts;

namespace Influence.GameClient
{
    public class ClientState
    {
        public Coordinate AttackFrom;
        public Coordinate AttackTo;
        public Coordinate Reinforce;
        public Session Session;
        public string PlayerId;

        public PlayerState CurrentPlayerState
        {
            get
            {
                var currentPlayerId = Session?.GameState?.CurrentPlayer?.Id.ToString();
                if (currentPlayerId == null || !PlayerId.Equals(currentPlayerId))
                    return PlayerState.Waiting;
                if (PlayerPhase.MoveAndAttack.Equals(Session.GameState.PlayerPhase))
                    return PlayerState.Attacking;
                return PlayerState.Reinforcing;
            }
        }

        public int BoardSize
            => Session?.CurrentBoard?.Size ?? 0;

        public bool CanAttack
            => CurrentPlayerState == PlayerState.Attacking && AttackFrom != null && AttackTo != null;

        public bool CanReinforce
            => CurrentPlayerState == PlayerState.Reinforcing && Reinforce != null;

        public string SessionId
            => Session?.Id.ToString() ?? string.Empty;

        public int AttackFromTileId
            => AttackFrom.CalculateId(BoardSize);

        public int AttackToTileId
            => AttackTo.CalculateId(BoardSize);

        public int ReinforceTileId
            => Reinforce.CalculateId(BoardSize);

        public void ClickCoordinate(int x, int y)
        {
            AttackFrom = null;
            AttackTo = null;
            Reinforce = null;
            if (CurrentPlayerState == PlayerState.Waiting)
                return;
            if (CurrentPlayerState == PlayerState.Reinforcing)
            {
                AttackFrom = null;
                Reinforce = new Coordinate(x, y);
            }
            else if (CurrentPlayerState == PlayerState.Attacking)
            {
                Reinforce = null;
                AttackFrom = new Coordinate(x, y);
            }
        }

        public void RightClickCoordinate(int x, int y)
        {
            if (CurrentPlayerState == PlayerState.Attacking)
                AttackTo = new Coordinate(x, y);
        }

        public enum PlayerState
        {
            Waiting,
            Attacking,
            Reinforcing
        }
    }
}
