using System;

namespace Influence.Domain
{
    public class Tile
    {
        public Guid OwnerId { get; set; }

        public string OwnerName { get; set; }
        public string OwnerColorRgbCsv { get; set; }
        public int NumTroops { get; set; }

        public int X { get; }
        public int Y { get;  }
        public int Id { get;  }
        public string Coordinates { get; }

        public Tile(int x, int y, int numColumns)
        {
            X = x;
            Y = y;
            Id = x + (y * numColumns);
            Coordinates = $"({x}, {y})";
        }

        // Deserialization of Tile.Id is buggy at this point. Re-create tile in memory to get proper Id.
        public static int ConstructTileId(int x, int y, int boardSize)
            => new Tile(x, y, boardSize).Id;

        public static int ConstructTileId(Tile t, int boardSize)
            => ConstructTileId(t.X, t.Y, boardSize);
    }
}