namespace Influence.Domain
{
    public class Coordinate
    {
        public Coordinate()
        { }

        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;
            Coordinates = $"({x}, {y})";
        }

        public int X { get; set; }
        public int Y { get; set;  }
        public string Coordinates { get; set; }
        public int CalculateId(int boardSize)
            => X + (boardSize * Y);
    }
}