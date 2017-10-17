namespace Influence.Domain
{
    public class RuleSet
    {
        public RuleSet(int boardSize, int numTroopsInStartTile, int maxNumTroopsInTile)
        {
            BoardSize = boardSize;
            NumTroopsInStartTile = numTroopsInStartTile;
            MaxNumTroopsInTile = maxNumTroopsInTile;
        }

        public int BoardSize { get; }
        public int NumTroopsInStartTile { get; }
        public int MaxNumTroopsInTile { get; }
    }
}