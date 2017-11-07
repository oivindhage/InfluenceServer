using Influence.GameClient.Mock;
using System;
using System.Drawing;
using System.Windows.Forms;
using Influence.Domain;
using System.Linq;
using System.Net;
using Newtonsoft.Json;
using RestSharp;

namespace Influence.GameClient
{
    public partial class GameClient : Form
    {
        private Graphics g;
        private Font font;
        private SolidBrush brush;
        private Pen pen;
        private StringFormat stringFormat;
        private int maxX;
        private int maxY;
        private int tileWidth;
        private int tileHeight;
        private Label targetForClick;

        public GameClient()
        {
            InitializeComponent();
            g = picBoard.CreateGraphics();
            var fontFamily = new FontFamily("Times New Roman");
            font = new Font(fontFamily, 16, FontStyle.Regular, GraphicsUnit.Pixel);
            brush = new SolidBrush(Color.FromArgb(255, 0, 0, 255));
            pen = new Pen(Color.Black);
            stringFormat = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
        }

        private void GameClient_Load(object sender, EventArgs e)
        {
            txtPlayerId.Text = Guid.NewGuid().ToString();
            txtPlayerName.Text = "Playername";
            txtSessionBaseUrl.Text = "http://localhost:82/ws.ashx";
        }

        private void DrawTile(Tile tile)
        {
            Color color = string.IsNullOrEmpty(tile.OwnerColorRgbCsv)
                ? Color.RosyBrown
                : DecodeRgb(tile.OwnerColorRgbCsv);
            string armyCountText = tile.NumTroops == 0
                ? string.Empty
                : $"{tile.NumTroops}";
            var rectangleF = new RectangleF(tile.X * tileWidth, tile.Y * tileHeight, tileWidth, tileHeight);
            g.FillRectangle(new SolidBrush(color), rectangleF);
            g.DrawRectangle(pen, rectangleF.X, rectangleF.Y, rectangleF.X + rectangleF.Width, rectangleF.Y + rectangleF.Height);
            g.DrawString(armyCountText, font, brush, rectangleF, stringFormat);
        }

        private Color DecodeRgb(string colorRgb)
        {
            int[] rgb = colorRgb
                .Split(',')
                .Select(x => int.Parse(x))
                .ToArray();
            return Color.FromArgb(rgb[0], rgb[1], rgb[2]);
        }

        private void btnDrawStatus_Click(object sender, EventArgs e)
        {
            var session = DummyService.GetDummySession();
            PresentSession(session);
        }

        private void PresentSession(Session session)
        {
            SetupTileMeasurements(session.CurrentBoard);
            DrawBoard(session.CurrentBoard);
            WritePlayerStatistics(session);
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
                rtxPlayerStatus.AppendText($"{player.Name}\n");
                rtxPlayerStatus.AppendText($"\tTiles: {participant.OwnedTiles.Count}\n");
                rtxPlayerStatus.AppendText($"\tTroops: {participant.OwnedTiles.Sum(x => x.NumTroops)}\n");
                rtxPlayerStatus.SelectionColor = rtxPlayerStatus.ForeColor;
                rtxPlayerStatus.SelectionFont = new Font(rtxPlayerStatus.Font, FontStyle.Regular);
            }
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

        private void btnConnectToSession_Click(object sender, EventArgs e)
        {
            if (!cmbCurrentGames.Enabled)
            {
                txtStatus.Text = "List sessions first";
                return;
            }
            var url = $"?join&session={cmbCurrentGames.Text}&playerid={txtPlayerId.Text}&name={txtPlayerName.Text}";
            var response = GetResponse(url);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                txtStatus.Text += response.StatusDescription;
                return;
            }
            txtStatus.Text += response.Content;
        }

        private void btnListAllSessions_Click(object sender, EventArgs e)
        {
            txtStatus.Text = string.Empty;
            var response = GetResponse("?sessions");
            dynamic converted = JsonConvert.DeserializeObject(response.Content);
            var sessionsJson = converted.Sessions.ToString();
            Session[] sessions = JsonConvert.DeserializeObject<Session[]>(sessionsJson);
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
        {
            if (!cmbCurrentGames.Enabled)
            {
                txtStatus.Text = "List sessions first";
                return;
            }
            var response = GetResponse($"?session={cmbCurrentGames.Text}");
            if (response.StatusCode != HttpStatusCode.OK)
            {
                txtStatus.Text += response.StatusDescription;
                return;
            }
            txtStatus.Text = response.Content;
            dynamic converted = JsonConvert.DeserializeObject(response.Content);
            var sessionJson = converted.Session.ToString();
            Session session = JsonConvert.DeserializeObject<Session>(sessionJson);
            PresentSession(session);
        }

        private IRestResponse GetResponse(string url)
            => new RestClient(txtSessionBaseUrl.Text).Get(new RestRequest(url));

        private void picBoard_Click(object sender, EventArgs e)
        {
            if (maxX == 0 || maxY == 0)
                return;
            MouseEventArgs me = (MouseEventArgs)e;
            Point coordinates = me.Location;
            int x = coordinates.X / (picBoard.Width / maxX);
            int y = coordinates.Y / (picBoard.Height / maxY);
            txtStatus.Text = $"Clicked on tile {x + 1},{y + 1}";
            if (targetForClick != null)
                targetForClick.Text = $"{x},{y}";
        }

        private void radioAttackFrom_CheckedChanged(object sender, EventArgs e)
            => targetForClick = lblAttackFrom;

        private void radioAttackDestination_CheckedChanged(object sender, EventArgs e)
            => targetForClick = lblAttackTo;

        private void radioReinforce_CheckedChanged(object sender, EventArgs e)
            => targetForClick = lblReinforce;

        private void btnCreateSession_Click(object sender, EventArgs e)
        {
            var response = GetResponse("?create");
            if (response.StatusCode != HttpStatusCode.OK)
                txtStatus.Text = response.StatusDescription;
            else
                txtStatus.Text = response.Content;
        }

        private void btnStartSession_Click(object sender, EventArgs e)
        {
            if (!cmbCurrentGames.Enabled)
            {
                txtStatus.Text = "List sessions first";
                return;
            }
            var response = GetResponse($"ws.ashx?start&session={cmbCurrentGames.Text}");
            if (response.StatusCode != HttpStatusCode.OK)
                txtStatus.Text = response.StatusDescription;
            else
                txtStatus.Text = response.Content;
        }
    }
}
