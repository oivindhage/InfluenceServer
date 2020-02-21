using System;
using System.Linq;
using System.Web.Mvc;
using Influence.Domain;
using Influence.Domain.Tournament;
using Influence.Services;
using Influence.Web.Models;

namespace Influence.Web.Controllers
{
    public class TournamentController : ControllerBase
    {
        public ActionResult Index()
        {
            var tournament = TournamentService.GetOrCreateTournament("The Legendary Tournament of Hilfe", GetWebServiceUrl());

            var model = new TournamentModel();

            var bots = GetUploadedBots();
            model.InvitedParticipants = bots.Select(b => new TournamentParticipant
            {
                Bot = b,
                Guid = GetBotGuid(b),
                HasJoined = tournament.Participants.Any(p => p.Bot.FolderName == b.FolderName)
            }).ToList();

            model.TournamentName = tournament.Name;
            model.Settings = tournament.Settings;
            model.CanSetupNextRound = (tournament.Rounds.LastOrDefault()?.IsComplete ?? false) && tournament.Rounds.Last().Sessions.Count > 1;
            model.CanPlayRound = tournament.Rounds.Any() && !tournament.Rounds.Last().IsStarted;
            model.Rounds = tournament.Rounds;

            return View(model);
        }


        private string GetWebServiceUrl()
            => $"{Request.Url.Scheme}://{Request.Url.Authority}/ws.ashx";


        private Guid GetBotGuid(UploadedBot uploadedBot)
            => new Guid(uploadedBot.FolderName.Substring(1 + uploadedBot.FolderName.LastIndexOf("\\")));


        [HttpPost]
        public ActionResult SaveConfig(TournamentModel model)
        {
            AssertValidConfigOrThrow(model);

            var tournament = TournamentService.CurrentTournament;
            tournament.Participants = model.InvitedParticipants.Where(b => b.HasJoined).ToList();
            tournament.Settings = model.Settings;
            tournament.Rounds.Clear();

            TournamentService.SetupNextRound();

            return RedirectToAction("Index");
        }


        [HttpPost]
        public ActionResult SetupNextRound(TournamentModel model)
        {
            TournamentService.SetupNextRound();
            return RedirectToAction("Index");
        }


        [HttpPost]
        public ActionResult PlayRound(TournamentModel model)
        {
            TournamentService.PlayRound();
            return RedirectToAction("Index");
        }


        private void AssertValidConfigOrThrow(in TournamentModel model)
        {
            if (model.Settings.MaxNumPlayersInEachGame < model.Settings.MinNumPlayersInEachGame)
                throw new Exception("Invalid: MaxNumPlayersInEachGame < MinNumPlayersInEachGame");

            if (model.Settings.MinNumPlayersInEachGame < 2 || model.Settings.MaxNumPlayersInEachGame < 2)
                throw new Exception("Min 2 players in each game");

            if (model.Settings.MaxNumPlayersInEachGame > RuleSet.Default.MaxNumPlayersInGame)
                throw new Exception($"Max {RuleSet.Default.MaxNumPlayersInGame} players in each game");
        }
    }
}