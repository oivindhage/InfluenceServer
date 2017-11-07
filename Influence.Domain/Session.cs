using System;
using System.Collections.Generic;
using System.Linq;
using Influence.Common.Utils;

namespace Influence.Domain
{
    public class Session
    {
        private static readonly List<string> PlayerColors = new List<string> { "255,0,0", "0,0,255", "0,255,0", "128,128,0" };

        public Guid Id { get; set; }
        public string Name { get; set; }

        public List<Player> Players { get; set; }
        public int RoundNumber { get; set; }
        public GameState GameState { get; set; }
        public RuleSet RuleSet { get; set; }
        public Board CurrentBoard { get; set; }

        public Session() { }

        public Session(RuleSet ruleSet, string name, Guid id = default(Guid))
        {
            Id = id == Guid.Empty ? Guid.NewGuid() : id;
            Players = new List<Player>();
            GameState = new GameState();
            RoundNumber = 0;
            RuleSet = ruleSet;
            Name = name;
            
            GenerateNewBoard();
        }

        public void GenerateNewBoard()
        {
            CurrentBoard = new Board(RuleSet);
        }

        public bool AddPlayer(Guid playerId, string playerName)
        {
            if (
                GameState.GamePhase == Consts.GamePhase.NotStarted &&
                playerId != Guid.Empty && Players.All(p => p.Id != playerId) &&
                Players.All(p => !p.Name.Equals(playerName, StringComparison.InvariantCultureIgnoreCase)) &&
                Players.Count < RuleSet.MaxNumPlayersInGame)
            {
                Players.Add(new Player(playerId, playerName, PlayerColors[Players.Count]));
                return true;
            }

            return false;
        }

        public bool Start()
        {
            if (Players == null || Players.Count < 2 || Players.Count > RuleSet.MaxNumPlayersInGame)
                return false;

            if (GameState.GamePhase != Consts.GamePhase.NotStarted)
                return false;

            GameState.GamePhase = Consts.GamePhase.Ongoing;

            Players.ForEach(p => p.NumAvailableReinforcements = 0);
            CurrentBoard.PlacePlayers(Players);
            RoundNumber = 1;

            GameState.Participants = new List<Participant>();
            Rng.ShuffleList(Players.ToList()).ForEach(p => GameState.Participants.Add(new Participant(p)));

            GameState.CurrentPlayer = GameState.Participants.First().Player;
            GameState.PlayerPhase = Consts.PlayerPhase.MoveAndAttack;

            return true;
        }

        public string Move(Guid playerId, int fromTileId, int toTileId, out string attackLog)
        {
            attackLog = string.Empty;

            if (GameState.GamePhase != Consts.GamePhase.Ongoing)
                return $"Flytting ikke tillatt i fasen {GameState.GamePhase}";

            var participant = GameState.Participants.FirstOrDefault(p => p.Player.Id == playerId);
            if (participant == null)
                return $"Det finnes ingen spiller med id {playerId} i denne session";

            if (!participant.IsAlive)
                return $"{participant.Player.Name} har ingen celler igjen i dette gamet";

            var player = participant.Player;

            if (GameState.CurrentPlayer.Id != player.Id)
                return $"Det er ikke {player.Name} sin tur";

            if (GameState.PlayerPhase != Consts.PlayerPhase.MoveAndAttack)
                return $"Nåværende fase er {GameState.PlayerPhase}, ikke {Consts.PlayerPhase.MoveAndAttack}";

            Participant deadDefender;
            string moveResult = CurrentBoard.Move(player, fromTileId, toTileId, GameState.Participants, out attackLog, out deadDefender);

            if (deadDefender != null)
            {
                deadDefender.Rank = GameState.Participants.Count(p => p.IsAlive);
                deadDefender.IsAlive = false;
            }

            return moveResult;
        }

        public string EndMove(Guid playerId)
        {
            if (GameState.GamePhase != Consts.GamePhase.Ongoing)
                return $"Avslutting av flytting ikke tillatt i fasen {GameState.GamePhase}";

            var participant = GameState.Participants.FirstOrDefault(p => p.Player.Id == playerId);
            if (participant == null)
                return $"Det finnes ingen spiller med id {playerId} i denne session";

            if (!participant.IsAlive)
                return $"{participant.Player.Name} har ingen celler igjen i dette gamet";

            var player = participant.Player;

            if (GameState.CurrentPlayer.Id != player.Id)
                return $"Det er ikke {player.Name} sin tur";

            if (GameState.PlayerPhase != Consts.PlayerPhase.MoveAndAttack)
                return $"Nåværende fase er {GameState.PlayerPhase}, ikke {Consts.PlayerPhase.MoveAndAttack}";

            GameState.PlayerPhase = Consts.PlayerPhase.Reinforce;

            CurrentBoard.GrantReinforcements(GameState.CurrentPlayer);

            return string.Empty;
        }

        public string Reinforce(Guid playerId, int tileId)
        {
            if (GameState.GamePhase != Consts.GamePhase.Ongoing)
                return $"Forsterking ikke tillatt i fasen {GameState.GamePhase}";

            var participant = GameState.Participants.FirstOrDefault(p => p.Player.Id == playerId);
            if (participant == null)
                return $"Det finnes ingen spiller med id {playerId} i denne session";

            if (!participant.IsAlive)
                return $"{participant.Player.Name} har ingen celler igjen i dette gamet";

            var player = participant.Player;

            if (GameState.CurrentPlayer.Id != player.Id)
                return $"Det er ikke {player.Name} sin tur";

            if (GameState.PlayerPhase != Consts.PlayerPhase.Reinforce)
                return $"Nåværende fase er {GameState.PlayerPhase}, ikke {Consts.PlayerPhase.Reinforce}";

            return CurrentBoard.Reinforce(player, tileId);
        }
    }
}