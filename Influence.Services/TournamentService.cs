using System;
using System.Collections.Generic;
using System.Linq;
using Influence.Common.Utils;
using Influence.Domain;
using Influence.Domain.Tournament;
using Influence.Services.Bot;

namespace Influence.Services
{
    public static class TournamentService
    {
        public static Tournament CurrentTournament { get; private set; }

        public static Tournament GetOrCreateTournament(string name, string webServiceUrl)
            => CurrentTournament ?? (CurrentTournament = new Tournament(name, webServiceUrl));

        public static void SetupNextRound()
        {
            if (CurrentTournament == null)
                return;

            var remainingParticipants = Rng.ShuffleList(CurrentTournament.Participants.Where(p => !p.IsEliminated).ToList());
            var sessions = CreateSessions(remainingParticipants, CurrentTournament.Settings);

            if (sessions.Any())
            {
                CurrentTournament.Rounds.Add(new TournamentRound
                {
                    RoundNumber = CurrentTournament.Rounds.Count + 1, 
                    Sessions = sessions
                });
            }

            // Only one participant left (a standin won in the other group(s))
            // Play the game immediately as there is no reason to wait
            if (sessions.Count == 1 && sessions[0].Players.Count == 1)
                PlayRound();
        }


        public static void PlayRound()
        {
            if (CurrentTournament == null || !CurrentTournament.Rounds.Any())
                return;

            var round = CurrentTournament.Rounds.Last();
            if (round.IsStarted || round.IsComplete)
                return;

            round.IsStarted = true;

            foreach (var session in round.Sessions)
                PlaySession(session);

            // todo ejay - wait until game is finished
            // DetermineWinnersOfRound(round);
            //
            // round.IsComplete = true;
        }


        private static void DetermineWinnersOfRound(TournamentRound round)
        {
            foreach (var participant in CurrentTournament.Participants)
            {
                var primarySessionWithParticipant = round.Sessions.FirstOrDefault(s => s.Players.Any(p => p.Id == participant.Guid && !p.IsStandInOnly));
                if (primarySessionWithParticipant != null)
                    participant.IsEliminated = primarySessionWithParticipant.GameState.Participants.First(p => p.Player.Id == participant.Guid).Rank != 1;
            }
        }


        private static void PlaySession(Session session)
        {
            if (session.Players.Count > 1)
            {
                var participants = GetParticipants(session);

                foreach (var participant in participants)
                {
                    var bot = participant.Bot;
                    BotService.HaveBotJoinGame(bot.FolderName, bot.Name, session.Id, CurrentTournament.WebServiceUrl, participant.Guid);
                }
            }
            
            session.Start();

            // todo ejay - wait until game is finished
            // for (int i = 0; i < session.GameState.Participants.Count; i++)
            // {
            //     var participant = session.GameState.Participants[i];
            //     participant.Rank = i+1;
            // }
        }


        private static List<TournamentParticipant> GetParticipants(Session session)
            => session.Players.Select(p => GetParticipant(session, p.Id)).ToList();
        
        private static TournamentParticipant GetParticipant(Session session, Guid playerId)
            => CurrentTournament.Participants.Single(p => p.Guid == playerId);


        private static List<Session> CreateSessions(List<TournamentParticipant> participants, TournamentSettings settings)
        {
            var participantsWithoutGroup = new List<TournamentParticipant>();
            participantsWithoutGroup.AddRange(participants);

            int sessionNum = 1;
            var sessions = new List<Session>();

            while (participantsWithoutGroup.Any())
            {
                var session = GameMaster.CreateSession(name: $"Game {sessionNum++}", isTournamentSession: true);

                while (participantsWithoutGroup.Any() && session.Players.Count < settings.MaxNumPlayersInEachGame)
                {
                    var part = participantsWithoutGroup[0];
                    session.AddPlayer(part.Guid, part.Bot.Name);
                    participantsWithoutGroup.RemoveAt(0);
                }

                sessions.Add(session);
            }

            int i = 0;
            var lastSession = sessions.LastOrDefault();
            while (lastSession != null && sessions.Count > 1 && lastSession.Players.Count < settings.MaxNumPlayersInEachGame)
            {
                var standInPlayer = participants.Skip(i++).First();
                lastSession.AddPlayer(standInPlayer.Guid, standInPlayer.Bot.Name, isStandInOnly: true);
            }
            
            return sessions;
        }
    }
}
