using System;

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

        public static readonly RuleSet Default = new RuleSet(boardSize: 6, numTroopsInStartTile:2, maxNumTroopsInTile:5);
    }
}