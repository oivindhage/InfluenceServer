using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Influence.Common.Extensions;
using Influence.Domain;
using Influence.SampleBot.Domain;
using Influence.Services;
using Timer = System.Windows.Forms.Timer;

namespace Influence.SampleBot
{
    public partial class Form1 : Form
    {
        private int stepIntervalMs = 500;

        // API documentation: /ws.ashx
        private readonly Gateway _gateway = new Gateway { SessionBaseUrl = "http://localhost:82/ws.ashx" };

        private string _sessionId;
        private Bot _bot1, _bot2;
        private List<Bot> _bots;

        private readonly Timer _stepTimer = new Timer();

        public Form1()
        {
            InitializeComponent();

            trackBar1.Value = stepIntervalMs;
            _stepTimer.Interval = stepIntervalMs;
            _stepTimer.Tick += (sender, args) => PerformStep();

            trackBar1_Scroll(null, null);
        }

        private void Log(string message) 
            => txtLog.AppendText($"{(txtLog.Text.IsEmpty() ? string.Empty : Environment.NewLine)}{DateTime.Now.ToLongTimeString()} > {message}");

        private void btnCreateGame_Click(object sender, EventArgs e)
        {
            _sessionId = Guid.NewGuid().ToString();
            txtLog.Text = string.Empty;

            var response = _gateway.Create(_sessionId);
            Log(response);
        }

        private void btnStartGame_Click(object sender, EventArgs e) 
            => Log(_gateway.StartSession(_sessionId));

        private void btnAddTwoBots_Click(object sender, EventArgs e)
        {
            _bot1 = new Bot("Bot 1");
            _bot2 = new Bot("Bot 2");
            _bots = new List<Bot> { _bot1, _bot2 };

            _bots.ForEach(bot => Log(_gateway.Join(_sessionId, bot.Id.ToString(), bot.Name)));
        }

        private void btnStep_Click(object sender, EventArgs e)
            => PerformStep();

        private void chkAutostep_CheckedChanged(object sender, EventArgs e)
        {
            _stepTimer.Enabled = chkAutostep.Checked;

            if (!_stepTimer.Enabled)
                _stepTimer.Stop();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            stepIntervalMs = _stepTimer.Interval = trackBar1.Value;
            lblStepDelayMs.Text = $"Interval {stepIntervalMs} ms";
        }

        private void PerformStep()
        {
            var session = _gateway.GetSession(_sessionId);

            if (session?.GameState.GamePhase != Consts.GamePhase.Ongoing)
                return;

            var gamestate = session.GameState;
            var activeBot = _bots.First(b => b.Id == gamestate.CurrentPlayer.Id);
            var board = session.CurrentBoard;
            var playersTiles = board.TilesOfPlayers[gamestate.CurrentPlayer.Id];

            if (gamestate.PlayerPhase == Consts.PlayerPhase.MoveAndAttack)
            {
                MoveInstruction instruction;
                if (playersTiles.Any(t => t.NumTroops > 1) && (instruction = activeBot.MoveAndAttack(playersTiles, board.Size, board.GetTile)) != null)
                {
                    Log(_gateway.Move(_sessionId, activeBot.Id.ToString(), instruction.SourceTileId, instruction.DestTileId));
                    return;
                }

                Log(_gateway.EndAttack(_sessionId, activeBot.Id.ToString()));
            }

            else if (gamestate.PlayerPhase == Consts.PlayerPhase.Reinforce)
            {
                ReinforceInstruction instruction;
                if (gamestate.CurrentPlayer.NumAvailableReinforcements > 0 && (instruction = activeBot.Reinforce(playersTiles, board.Size, board.RuleSet.MaxNumTroopsInTile)) != null)
                {
                    Log(_gateway.Reinforce(_sessionId, activeBot.Id.ToString(), instruction.TileId));
                    return;
                }

                Log(_gateway.EndReinforce(_sessionId, activeBot.Id.ToString()));
            }
        }
    }
}
