namespace Influence.Domain
{
    public class Coordinate
    {
        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;
            Coordinates = $"({x}, {y})";
        }

        public int X { get; }
        public int Y { get; }
        public string Coordinates { get; }
    }
}