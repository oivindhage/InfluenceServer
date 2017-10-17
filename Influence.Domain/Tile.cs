using System;
using Newtonsoft.Json;

namespace Influence.Domain
{
    public class Tile
    {
        [JsonIgnore]
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
    }
}