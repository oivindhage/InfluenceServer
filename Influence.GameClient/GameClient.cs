using Influence.GameClient.Mock;
using System;
using System.Drawing;
using System.Windows.Forms;
using Influence.Domain;
using System.Linq;
using System.Net;
using System.IO;
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
            txtSessionBaseUrl.Text = "http://osl-ejay.co.int:84/ws.ashx";
            txtSessionGuid.Text = Guid.NewGuid().ToString();
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
            var board = session.Board;
            SetupTileMeasurements(board);
            DrawBoard(board);
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
            foreach (var tileRow in board.TileRows)
                foreach (var tile in tileRow.Tiles)
                    DrawTile(tile);
        }

        private void SetupTileMeasurements(Board board)
        {
            maxY = board.TileRows.Count;
            maxX = board.TileRows.First().Tiles.Count;
            tileWidth = picBoard.Width / maxX;
            tileHeight = picBoard.Height / maxY;
        }

        private void btnConnectToSession_Click(object sender, EventArgs e)
        {
            var url = $"?join&session={txtSessionGuid.Text}&playerid={txtPlayerId.Text}&name={txtPlayerName.Text}";
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
                cmbCurrentGames.Enabled = true;
                cmbCurrentGames.Items.Insert(0, session.Id.ToString());
            }
        }

        private void btnShowSessionDetails_Click(object sender, EventArgs e)
        {
            var response = GetResponse($"?session={txtSessionGuid.Text}");
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

        private void cmbCurrentGames_SelectedIndexChanged(object sender, EventArgs e)
            => txtSessionGuid.Text = cmbCurrentGames.Text;
    }
}
