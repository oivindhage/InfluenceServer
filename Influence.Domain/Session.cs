using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Influence.Common.Extensions;
using Influence.Common.Utils;

namespace Influence.Domain
{
    public class Session
    {
        private static readonly List<string> PlayerColors = new List<string> { "255,0,0", "0,0,255", "0,255,0", "128,128,0" };

        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreationTime { get; }

        public List<Player> Players { get; set; }
        public int RoundNumber { get; set; }
        public GameState GameState { get; set; }
        public RuleSet RuleSet { get; set; }
        public Board CurrentBoard { get; set; }

        public Session() { }

        public Session(RuleSet ruleSet, string name, Guid id = default)
        {
            Id = id == Guid.Empty ? Guid.NewGuid() : id;
            Name = name;
            CreationTime = DateTime.Now;

            Players = new List<Player>();
            GameState = new GameState();
            RoundNumber = 0;
            RuleSet = ruleSet;
        }


        private void Log(string message)
        {
            return;
            try { System.IO.File.AppendAllText("C:\\InfluenceLogs\\Session log.txt", message + Environment.NewLine); }
            catch { }
        }


        public bool AddPlayer(Guid playerId, string playerName, bool isStandInOnly = false)
        {
            if (
                GameState.GamePhase == Consts.GamePhase.NotStarted &&
                playerId != Guid.Empty && Players.All(p => p.Id != playerId) &&
                Players.All(p => !p.Name.Equals(playerName, StringComparison.InvariantCultureIgnoreCase)) &&
                Players.Count < RuleSet.MaxNumPlayersInGame)
            {
                Players.Add(new Player(playerId, playerName, PlayerColors[Players.Count], isStandInOnly: isStandInOnly));
                return true;
            }

            return false;
        }

        public bool Start()
        {
            Log("Start()");

            if (Players == null || Players.Count < 2 || Players.Count > RuleSet.MaxNumPlayersInGame)
                return false;

            if (GameState.GamePhase != Consts.GamePhase.NotStarted)
                return false;

            RoundNumber++;
            
            CurrentBoard = new Board(RuleSet);
            CurrentBoard.PlacePlayers(Players);

            Players.ForEach(p => p.NumAvailableReinforcements = 0);
            GameState.Participants = new List<Participant>();
            Rng.ShuffleList(Players.ToList()).ForEach(p => GameState.Participants.Add(new Participant(p)));
            GiveTurnToNextPlayer();

            GameState.GamePhase = Consts.GamePhase.Ongoing;

            return true;
        }

        public bool NewGame()
        {
            Log("NewGame()");

            if (GameState.GamePhase != Consts.GamePhase.Finished)
                return false;

            GameState.GamePhase = Consts.GamePhase.NotStarted;

            return Start();
        }

        public void GiveTurnToNextPlayer()
        {
            Log("GiveTurnToNextPlayer()");

            GameState.PlayerPhase = Consts.PlayerPhase.MoveAndAttack;

            if (GameState.CurrentPlayer == null)
                GameState.CurrentPlayer = GameState.Participants.Random().Player;
            else
            {
                if (GameState.Participants.Count(p => p.IsAlive) == 1)
                    return;

                int idxNextPlayer = GameState.Participants.FindIndex(p => p.Player.Id == GameState.CurrentPlayer.Id);
                do
                {
                    idxNextPlayer++;
                    if (idxNextPlayer >= GameState.Participants.Count)
                        idxNextPlayer = 0;
                }
                while (!GameState.Participants[idxNextPlayer].IsAlive);

                GameState.CurrentPlayer = GameState.Participants[idxNextPlayer].Player;
            }
        }

        private void AwardScoreToParticipants(IEnumerable<Participant> participants)
        {
            Log("AwardScoreToParticipants()");

            foreach (var participant in participants)
            {
                if (participant.Rank == 1)
                {
                    participant.Player.Score += 5;
                    participant.Player.NumPos1++;
                }
                else if (participant.Rank == 2)
                {
                    participant.Player.Score += 3;
                    participant.Player.NumPos2++;
                }
                else if (participant.Rank == 3)
                {
                    participant.Player.Score += 2;
                    participant.Player.NumPos3++;
                }
                else
                {
                    participant.Player.Score += 0;
                    participant.Player.NumPos4++;
                }
            }
        }

        public string Move(Guid playerId, int fromTileId, int toTileId, out string attackLog)
        {
            Log($"Move() - playerId {playerId} - fromTileId {fromTileId} - toTileId {toTileId}");

            attackLog = string.Empty;

            if (GameState.GamePhase != Consts.GamePhase.Ongoing)
                return $"Flytting ikke mulig i spillfasen {GameState.GamePhase}";

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

                if (GameState.Participants.Count(p => p.IsAlive) == 1)
                {
                    // Give last survivor aka winner Rank 1
                    GameState.Participants.First(p => p.IsAlive).Rank = 1;

                    GameState.GamePhase = Consts.GamePhase.Finished;
                    GameState.PlayerPhase = Consts.PlayerPhase.NotAvailable;
                    AwardScoreToParticipants(GameState.Participants);
                }
            }

            return moveResult;
        }

        public string EndMove(Guid playerId)
        {
            Log($"EndMove() - playerId {playerId}");

            if (GameState.GamePhase != Consts.GamePhase.Ongoing)
                return $"Avslutting av flyttefase ikke mulig i spillfasen {GameState.GamePhase}";

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

        public string Reinforce(Guid playerId, int tileId, out string reinforceLog)
        {
            Log($"Reinforce() - playerId {playerId} - tileId {tileId}");

            reinforceLog = string.Empty;

            if (GameState.GamePhase != Consts.GamePhase.Ongoing)
                return $"Forsterking ikke mulig i spillfasen {GameState.GamePhase}";

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

            return CurrentBoard.Reinforce(player, tileId, out reinforceLog);
        }

        public string EndReinforce(Guid playerId)
        {
            Log($"EndReinforce() - playerId {playerId}");

            if (GameState.GamePhase != Consts.GamePhase.Ongoing)
                return $"Avslutting av forsterkningsfase ikke mulig i spillfasen {GameState.GamePhase}";

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

            GiveTurnToNextPlayer();

            return string.Empty;
        }
    }
}