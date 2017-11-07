using System;
using System.Drawing;
using System.Windows.Forms;
using Influence.Domain;
using System.Linq;
using static Influence.GameClient.ClientState;

namespace Influence.GameClient
{
    public partial class GameClient : Form
    {
        private Graphics g;
        private Font font;
        private Pen pen;
        private StringFormat stringFormat;
        private int maxX;
        private int maxY;
        private int tileWidth;
        private int tileHeight;
        private Label targetForClick;
        private ClientState clientState;
        private InfluenceGateway influenceGateway;

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
            influenceGateway = new InfluenceGateway();
            txtSessionBaseUrl.Text = ConfigurationSettings.ServerUrl;
        }

        private void DrawTile(Tile tile)
        {
            Color color = string.IsNullOrEmpty(tile.OwnerColorRgbCsv)
                ? Color.RosyBrown
                : DecodeRgb(tile.OwnerColorRgbCsv);
            var rectangleF = new RectangleF(tile.X * tileWidth, tile.Y * tileHeight, tileWidth, tileHeight);
            g.FillRectangle(new SolidBrush(color), rectangleF);
            g.DrawRectangle(pen, rectangleF.X, rectangleF.Y, rectangleF.X + rectangleF.Width, rectangleF.Y + rectangleF.Height);
            if (tile.NumTroops == 0)
                return;
            var brush = GetBrush(color);
            g.DrawString($"{tile.NumTroops}", font, brush, rectangleF, stringFormat);
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
                string extraPlayerInfo = string.Empty;
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
            {
                txtStatus.Text = "List sessions first";
                return;
            }
            clientState.Session = influenceGateway.GetSession(cmbCurrentGames.Text);
            PresentSession();
        }

        private void picBoard_Click(object sender, EventArgs e)
        {
            if (maxX == 0 || maxY == 0)
                return;
            MouseEventArgs me = (MouseEventArgs)e;
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
                Attack();
            else if (clientState.CanReinforce)
                Reinforce();
        }

        private void Reinforce()
            => influenceGateway.Reinforce(clientState.SessionId, txtPlayerId.Text, clientState.ReinforceTileId);

        private void Attack()
            => influenceGateway.Move(clientState.SessionId, txtPlayerId.Text, clientState.AttackFromTileId, clientState.AttackToTileId);

        private void radioAttackFrom_CheckedChanged(object sender, EventArgs e)
            => targetForClick = lblAttackFrom;

        private void radioAttackDestination_CheckedChanged(object sender, EventArgs e)
            => targetForClick = lblAttackTo;

        private void radioReinforce_CheckedChanged(object sender, EventArgs e)
            => targetForClick = lblReinforce;

        private void btnCreateSession_Click(object sender, EventArgs e)
            => txtStatus.Text += influenceGateway.Create();

        private void btnStartSession_Click(object sender, EventArgs e)
        {
            if (!cmbCurrentGames.Enabled)
            {
                txtStatus.Text = "List sessions first";
                return;
            }
            txtStatus.Text = influenceGateway.StartSession(cmbCurrentGames.Text);
            if (!chkAutoUpdateUi.Checked)
                chkAutoUpdateUi.Checked = true;
        }

        private void chkAutoUpdateUi_CheckedChanged(object sender, EventArgs e)
            => tmrPoll.Enabled = !tmrPoll.Enabled;

        private void tmrPoll_Tick(object sender, EventArgs e)
            => RefreshState();

        private void btnEndAttack_Click(object sender, EventArgs e)
            => txtStatus.Text += influenceGateway.EndAttack(clientState.SessionId, txtPlayerId.Text);

        private void btnEndReinforce_Click(object sender, EventArgs e)
            => txtStatus.Text += influenceGateway.EndReinforce(clientState.SessionId, txtPlayerId.Text);

        private void txtSessionBaseUrl_TextChanged(object sender, EventArgs e)
            => influenceGateway.SessionBaseUrl = txtSessionBaseUrl.Text;

        private void txtPlayerId_TextChanged(object sender, EventArgs e)
            => clientState.PlayerId = txtPlayerId.Text;
    }
}
