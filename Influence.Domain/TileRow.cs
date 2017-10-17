using System.Collections.Generic;

namespace Influence.Domain
{
    public class TileRow
    {
        public int RowNum { get;  }
        public List<Tile> Tiles { get; } = new List<Tile>();

        public TileRow(int rowNum)
        {
            RowNum = rowNum;
        }
    }
}