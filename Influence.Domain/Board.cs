using System.Collections.Generic;

namespace Influence.Domain
{
    public class Board
    {
        public List<TileRow> TileRows { get; set; }

        public Board(int size)
        {
            TileRows = new List<TileRow>();
        }
    }
}
