using System;
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
        {
            get
            {
                if (CurrentPlayerState != PlayerState.Attacking || AttackFrom is null || AttackTo is null)
                    return false;
                var attackFromTile = Session.CurrentBoard.TileRows[AttackFrom.Y].Tiles[AttackFrom.X];
                if (attackFromTile.OwnerId != Guid.Parse(PlayerId))
                    return false;
                if (attackFromTile.NumTroops < 2)
                    return false;
                var attackToTile = Session.CurrentBoard.TileRows[AttackTo.Y].Tiles[AttackTo.X];
                if (attackToTile.OwnerId == Guid.Parse(PlayerId))
                    return false;
                if (!AreAdjacent(AttackFrom, AttackTo))
                    return false;
                return true;
            }
        }

        private bool AreAdjacent(Coordinate attackFrom, Coordinate attackTo)
        {
            if (attackFrom.X == attackTo.X && Math.Abs(attackFrom.Y - attackTo.Y) == 1)
                return true;
            if (attackFrom.Y == attackTo.Y && Math.Abs(attackFrom.X - attackTo.X) == 1)
                return true;
            return false;
        }

        public bool CanReinforce
        {
            get
            {
                if (CurrentPlayerState != PlayerState.Reinforcing || Reinforce is null)
                    return false;
                var reinforceTile = Session.CurrentBoard.TileRows[Reinforce.Y].Tiles[Reinforce.X];
                return reinforceTile.NumTroops != 5 && reinforceTile.OwnerId == Guid.Parse(PlayerId);
            }
        }

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

        public void ResetCoordinates()
        {
            AttackFrom = null;
            AttackTo = null;
            Reinforce = null;
        }
    }
}
