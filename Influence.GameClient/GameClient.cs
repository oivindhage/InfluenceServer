using System;
using System.Drawing;
using System.Windows.Forms;
using Influence.Domain;
using System.Linq;
using Influence.Services;
using static Influence.GameClient.ClientState;
using Influence.SampleBot.Domain;

namespace Influence.GameClient
{
    public partial class GameClient : Form
    {
        private readonly Graphics g;
        private readonly Font font;
        private readonly Pen pen;
        private readonly StringFormat stringFormat;
        private int maxX;
        private int maxY;
        private int tileWidth;
        private int tileHeight;
        private readonly ClientState clientState;
        private Gateway influenceGateway;
        private Bot Bot1;

        public GameClient()
        {
            InitializeComponent();
            g = picBoard.CreateGraphics();
            var fontFamily = new FontFamily("Times New Roman");
            font = new Font(fontFamily, 16, FontStyle.Regular, GraphicsUnit.Pixel);
            pen = new Pen(Color.Black);
            stringFormat = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
            clientState = new ClientState();
        }

        private void GameClient_Load(object sender, EventArgs e)
        {
            txtPlayerId.Text = Guid.NewGuid().ToString();
            txtPlayerName.Text = ConfigurationSettings.PlayerName;
            influenceGateway = new Gateway();
            txtSessionBaseUrl.Text = ConfigurationSettings.ServerUrl;
        }

        private void DrawTile(Tile tile)
        {
            Color color = string.IsNullOrEmpty(tile.OwnerColorRgbCsv)
                ? Color.RosyBrown
                : DecodeRgb(tile.OwnerColorRgbCsv);
            RectangleF rectangleF = GetRectangle(tile.X, tile.Y);
            g.FillRectangle(new SolidBrush(color), rectangleF);
            g.DrawRectangle(pen, rectangleF.X, rectangleF.Y, rectangleF.Width, rectangleF.Height);
            if (tile.NumTroops == 0)
                return;
            var brush = GetBrush(color);
            g.DrawString($"{tile.NumTroops}", font, brush, rectangleF, stringFormat);
        }

        private RectangleF GetRectangle(int x, int y)
            => new RectangleF(x * tileWidth, y * tileHeight, tileWidth, tileHeight);

        private void DrawBrightFrame(Coordinate attackFrom)
        {
            if (attackFrom is null)
                return;
            var rectangleF = GetRectangle(attackFrom.X, attackFrom.Y);
            var whitePen = new Pen(new SolidBrush(Color.White));
            g.DrawRectangle(whitePen, rectangleF.X, rectangleF.Y, rectangleF.Width, rectangleF.Height);
        }

        private SolidBrush GetBrush(Color color)
            => color.R > 200 || color.G > 200
                ? new SolidBrush(Color.Black)
                : new SolidBrush(Color.White);

        private Color DecodeRgb(string colorRgb)
        {
            int[] rgb = colorRgb
                .Split(',')
                .Select(x => int.Parse(x))
                .ToArray();
            return Color.FromArgb(rgb[0], rgb[1], rgb[2]);
        }

        private void PresentSession()
        {
            SetupTileMeasurements(clientState.Session.CurrentBoard);
            DrawBoard(clientState.Session.CurrentBoard);
            WritePlayerStatistics(clientState.Session);
            SetCorrectButtonStates(clientState);
        }

        private void SetCorrectButtonStates(ClientState clientState)
        {
            btnEndAttack.Enabled = clientState.CurrentPlayerState == PlayerState.Attacking;
            btnEndReinforce.Enabled = clientState.CurrentPlayerState == PlayerState.Reinforcing;
        }

        private void WritePlayerStatistics(Session session)
        {
            if (session.GameState.GamePhase.Equals(Consts.GamePhase.NotStarted))
                return;
            rtxPlayerStatus.Text = string.Empty;
            foreach (var player in session.Players)
            {
                var participant = session.GameState.Participants.First(x => x.Player.Id.Equals(player.Id));
                rtxPlayerStatus.SelectionStart = rtxPlayerStatus.TextLength;
                rtxPlayerStatus.SelectionLength = 0;
                rtxPlayerStatus.SelectionColor = DecodeRgb(player.ColorRgbCsv);
                if (session.GameState.CurrentPlayer.Id.Equals(player.Id))
                    rtxPlayerStatus.SelectionFont = new Font(rtxPlayerStatus.Font, FontStyle.Bold);
                var participantTiles = session.CurrentBoard.GetTilesOfPlayer(participant.Player);
                var extraPlayerInfo = string.Empty;
                if (txtPlayerId.Text.Equals(player.Id.ToString(), StringComparison.InvariantCultureIgnoreCase))
                    extraPlayerInfo = " (you)";
                if (session.GameState.CurrentPlayer.Id.Equals(player.Id))
                    extraPlayerInfo += " <- This players turn";
                rtxPlayerStatus.AppendText($"{player.Name}{extraPlayerInfo}\n");
                rtxPlayerStatus.AppendText($"\tTiles: {participantTiles.Count()}\n");
                rtxPlayerStatus.AppendText($"\tTroops: {participantTiles.Sum(x => x.NumTroops)}\n");
                rtxPlayerStatus.SelectionColor = rtxPlayerStatus.ForeColor;
                rtxPlayerStatus.SelectionFont = new Font(rtxPlayerStatus.Font, FontStyle.Regular);
            }
            if (clientState.CurrentPlayerState == PlayerState.Waiting)
                rtxPlayerStatus.AppendText("The current player is awaiting his turn.");
            else if (clientState.CurrentPlayerState == PlayerState.Attacking)
            {
                if (clientState.AttackFrom == null)
                    rtxPlayerStatus.AppendText("The current player is attacking.");
                else
                    rtxPlayerStatus.AppendText($"The current player is attacking from ({clientState.AttackFrom.X},{clientState.AttackFrom.Y}).");
            }
            else if (clientState.CurrentPlayerState == PlayerState.Reinforcing)
                rtxPlayerStatus.AppendText("The current player is reinforcing.");

        }

        private void DrawBoard(Board board)
        {
            if (board == null)
                return;
            foreach (var tileRow in board.TileRows)
                foreach (var tile in tileRow.Tiles)
                    DrawTile(tile);
            DrawBrightFrame(clientState.AttackFrom);
        }

        private void SetupTileMeasurements(Board board)
        {
            if (board == null)
                return;
            maxY = board.TileRows.Count;
            maxX = board.TileRows.First().Tiles.Count;
            tileWidth = picBoard.Width / maxX;
            tileHeight = picBoard.Height / maxY;
        }

        private void btnJoinSession_Click(object sender, EventArgs e)
        {
            if (!cmbCurrentGames.Enabled)
            {
                txtStatus.Text = "List sessions first";
                return;
            }
            txtStatus.Text += influenceGateway.Join(cmbCurrentGames.Text, txtPlayerId.Text, txtPlayerName.Text);
        }

        private void btnListAllSessions_Click(object sender, EventArgs e)
        {
            txtStatus.Text = string.Empty;
            Session[] sessions = influenceGateway.GetSessions();
            cmbCurrentGames.Items.Clear();
            foreach (var session in sessions)
            {
                txtStatus.Text += $"Session: {session.Id}, Players: {session.Players.Count}{Environment.NewLine}";
                cmbCurrentGames.Items.Insert(0, session.Id.ToString());
            }
            if (cmbCurrentGames.Items.Count > 0)
            {
                cmbCurrentGames.SelectedIndex = 0;
                cmbCurrentGames.Enabled = true;
            }
        }

        private void btnShowSessionDetails_Click(object sender, EventArgs e)
            => RefreshState();

        private void RefreshState()
        {
            if (!cmbCurrentGames.Enabled)
                return;
            clientState.Session = influenceGateway.GetSession(cmbCurrentGames.Text);
            if (clientState.Session is null)
                return;
            PresentSession();
            if (chkPlayAsBot.Checked)
                PlayAsBot();
        }

        private void PlayAsBot()
        {
            CreateBotIfNotExists();
            if (clientState.Session.GameState.GamePhase != Consts.GamePhase.Ongoing)
                return;
            if (clientState.PlayerId != clientState.Session.GameState.CurrentPlayer.Id.ToString())
                return;
            if (clientState.Session.GameState.PlayerPhase == Consts.PlayerPhase.MoveAndAttack)
            {
                var move = Bot1.MoveAndAttack(clientState.Session);
                if (move is null)
                    EndAttack();
                else
                    Attack(Tile.ConstructTileId(move.SourceTile, clientState.Session.RuleSet.BoardSize), Tile.ConstructTileId(move.DestinationTile, clientState.Session.RuleSet.BoardSize));
            }
            else if (clientState.Session.GameState.PlayerPhase == Consts.PlayerPhase.Reinforce)
            {
                var reinforce = Bot1.Reinforce(clientState.Session);
                if (reinforce is null)
                    EndReinforce();
                else
                    Reinforce(Tile.ConstructTileId(reinforce, clientState.Session.RuleSet.BoardSize));
            }
        }

        private void CreateBotIfNotExists()
        {
            if (Bot1 is null)
                Bot1 = new Bot(txtPlayerName.Text, Guid.Parse(txtPlayerId.Text));
        }

        private void picBoard_Click(object sender, EventArgs e)
        {
            if (maxX == 0 || maxY == 0)
                return;
            var me = (MouseEventArgs)e;
            Point coordinates = me.Location;
            int x = coordinates.X / (picBoard.Width / maxX);
            int y = coordinates.Y / (picBoard.Height / maxY);
            if (me.Button == MouseButtons.Left)
            {
                txtStatus.Text = $"Leftclicked on tile {x + 1},{y + 1}";
                clientState.ClickCoordinate(x, y);
            }
            else if (me.Button == MouseButtons.Right)
            {
                txtStatus.Text = $"Rightclicked on tile {x + 1},{y + 1}";
                clientState.RightClickCoordinate(x, y);
            }
            if (clientState.CanAttack)
                Attack(clientState.AttackFromTileId, clientState.AttackToTileId);
            else if (clientState.CanReinforce)
                Reinforce(clientState.ReinforceTileId);
        }

        private void btnCreateSession_Click(object sender, EventArgs e)
            => txtStatus.Text += influenceGateway.Create();

        private void btnStartSession_Click(object sender, EventArgs e) 
            => txtStatus.Text = cmbCurrentGames.Enabled 
                ? influenceGateway.StartSession(cmbCurrentGames.Text) 
                : "List sessions first";

        private void tmrPoll_Tick(object sender, EventArgs e)
            => RefreshState();

        private void btnEndAttack_Click(object sender, EventArgs e)
            => txtStatus.Text += EndAttack();

        private void btnEndReinforce_Click(object sender, EventArgs e)
            => txtStatus.Text += EndReinforce();

        private void txtSessionBaseUrl_TextChanged(object sender, EventArgs e)
            => influenceGateway.SessionBaseUrl = txtSessionBaseUrl.Text;

        private void txtPlayerId_TextChanged(object sender, EventArgs e)
            => clientState.PlayerId = txtPlayerId.Text;

        private string EndReinforce()
            => influenceGateway.EndReinforce(clientState.SessionId, txtPlayerId.Text);

        private string EndAttack()
            => influenceGateway.EndAttack(clientState.SessionId, txtPlayerId.Text);

        private void Reinforce(int reinforceTileId)
        {
            influenceGateway.Reinforce(clientState.SessionId, txtPlayerId.Text, reinforceTileId);
            clientState.ResetCoordinates();
        }

        private void Attack(int fromTileId, int toTileId)
        {
            influenceGateway.Move(clientState.SessionId, txtPlayerId.Text, fromTileId, toTileId);
            clientState.ResetCoordinates();
        }
    }
}
