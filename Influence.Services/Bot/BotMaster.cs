using Influence.Domain;
using System;
using System.Threading;

namespace Influence.Services.Bot
{
    public class BotMaster
    {
        private readonly Gateway _gateway;
        private readonly Guid _playerGuid;
        private readonly string _botName;
        private readonly string _sessionId;
        private readonly SampleBot _bot;
        private bool _isRunning;

        public BotMaster(string botName, string serverUrl, string sessionId)
        {
            _gateway = new Gateway { SessionBaseUrl = serverUrl };
            _playerGuid = Guid.NewGuid();
            _botName = botName;
            _sessionId = sessionId;
            _bot = new SampleBot(_botName, _playerGuid);
        }

        public void Run()
        {
            _gateway.Join(_sessionId, _playerGuid.ToString(), _botName);
            _isRunning = true;
            while (_isRunning)
                Poll();
        }

        private void Poll()
        {
            var session = _gateway.GetSession(_sessionId);
            if (session.GameState.GamePhase == Consts.GamePhase.Finished)
                _isRunning = false;
            if (session.GameState.GamePhase != Consts.GamePhase.Ongoing)
                return;
            if (!_playerGuid.Equals(session.GameState.CurrentPlayer.Id))
                return;
            if (session.GameState.PlayerPhase == Consts.PlayerPhase.MoveAndAttack)
                Move(session);
            else if (session.GameState.PlayerPhase == Consts.PlayerPhase.Reinforce)
                Reinforce(session);
            Thread.Sleep(100);
        }

        private void Reinforce(Session session)
        {
            var reinforce = _bot.GetTileToReinforce(session);
            if (reinforce is null)
                _gateway.EndReinforce(_sessionId, _playerGuid.ToString());
            else
                _gateway.Reinforce(_sessionId, _playerGuid.ToString(), Tile.ConstructTileId(reinforce, session.RuleSet.BoardSize));
        }

        private void Move(Session session)
        {
            var move = _bot.GetMoveOrAttackInstruction(session);
            if (move is null)
                _gateway.EndAttack(_sessionId, _playerGuid.ToString());
            else
                _gateway.Move(_sessionId, _playerGuid.ToString(), Tile.ConstructTileId(move.SourceTile, session.RuleSet.BoardSize), Tile.ConstructTileId(move.DestinationTile, session.RuleSet.BoardSize));
        }
    }
}
