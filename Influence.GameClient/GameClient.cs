using Influence.GameClient.Mock;
using System;
using System.Drawing;
using System.Windows.Forms;
using Influence.Domain;
using System.Linq;

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
        }

        private void btnDrawTiles_Click(object sender, EventArgs e)
        {
            var session = DummyService.GetDummySession();
            var board = session.Board;
            maxY = board.TileRows.Count;
            maxX = board.TileRows.First().Tiles.Count;
            tileWidth = picBoard.Width / maxX;
            tileHeight = picBoard.Height / maxY;
            foreach (var tileRow in board.TileRows)
                foreach (var tile in tileRow.Tiles)
                    DrawTile(tile);
        }

        private void DrawTile(Tile tile)
        {
            Color color = Color.SeaShell;
            string armyCountText = tile.NumTroops == 0
                ? string.Empty
                : $"{tile.NumTroops}";
            var rectangleF = new RectangleF(tile.X * tileWidth, tile.Y * tileHeight, tileWidth, tileHeight);
            g.FillRectangle(new SolidBrush(color), rectangleF);
            g.DrawRectangle(pen, rectangleF.X, rectangleF.Y, rectangleF.X + rectangleF.Width, rectangleF.Y + rectangleF.Height);
            g.DrawString(armyCountText, font, brush, rectangleF, stringFormat);
        }
    }
}
