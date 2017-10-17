using Influence.GameClient.Mock;
using System;
using System.Drawing;
using System.Windows.Forms;
using Influence.Domain;
using System.Linq;
using System.Net;
using System.IO;

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
            txtClientId.Text = Guid.NewGuid().ToString();
            txtPlayerNick.Text = "Playername";
            txtSessionUrl.Text = "http://osl-ejay.co.int:84/ws.ashx?join&session=<sessionId>&playerid=<playerId>&name=<name>";
            txtSessionGuid.Text = Guid.NewGuid().ToString();
        }

        private void DrawTile(Tile tile)
        {
            //Color color = string.IsNullOrEmpty(tile.ColorRgb)
            //    ? Color.SeaShell
            //    : DecodeRgb(tile.ColorRgb);
            Color color = Color.SaddleBrown;
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
            var board = session.Board;
            SetupTileMeasurements(board);
            DrawBoard(board);
            WritePlayerStatistics(session);
        }

        private void WritePlayerStatistics(Session session)
        {
            var currentColor = rtxPlayerStatus.ForeColor;
            foreach (var player in session.Players)
            {
                rtxPlayerStatus.ForeColor = DecodeRgb(player.ColorRgb);
                var participant = session.GameState.Participants.First(x => x.Player.Id.Equals(player.Id));
                if (session.GameState.CurrentPlayer.Id.Equals(player.Id))
                    rtxPlayerStatus.Font = new Font(rtxPlayerStatus.Font, FontStyle.Bold);
                else
                    rtxPlayerStatus.Font = new Font(rtxPlayerStatus.Font, FontStyle.Regular);
                rtxPlayerStatus.AppendText($"{player.Nick}\n\tTiles: {participant.OwnedTiles}\n\tTroops: {participant.OwnedTiles.Sum(x => x.NumTroops)}");
            }
            rtxPlayerStatus.Font = new Font(rtxPlayerStatus.Font, FontStyle.Regular);
            rtxPlayerStatus.ForeColor = currentColor;
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
            var request = WebRequest.Create("");
            using (var response = request.GetResponse())
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                txtStatus.Text = reader.ReadToEnd();
            }
        }
    }
}
