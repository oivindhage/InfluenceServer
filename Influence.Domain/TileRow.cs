using System.Collections.Generic;

namespace Influence.Domain
{
    public class TileRow
    {
        public int RowNum { get; set; }
        public List<Tile> Tiles { get; set; } = new List<Tile>();

        public TileRow()
        { }

        public TileRow(int rowNum)
        {
            RowNum = rowNum;
        }
    }
}