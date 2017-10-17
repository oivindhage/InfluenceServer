using System.Collections.Generic;

namespace Influence.Domain
{
    public class Board
    {
        public List<TileRow> TileRows { get; } = new List<TileRow>();

        public Board(int size)
        {
            for (int rowNum = 0; rowNum < size; rowNum++)
            {
                var row = new TileRow(rowNum);

                for (int colNum = 0; colNum < size; colNum++)
                {
                    var tile = new Tile(colNum, rowNum, size);
                    row.Tiles.Add(tile);
                }

                TileRows.Add(row);
            }
        }
    }
}
