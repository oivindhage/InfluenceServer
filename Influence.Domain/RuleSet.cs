namespace Influence.Domain
{
    public class RuleSet
    {
        public RuleSet(int boardSize, int numTroopsInStartTile, int maxNumTroopsInTile, int maxNumPlayersInGame)
        {
            BoardSize = boardSize;
            NumTroopsInStartTile = numTroopsInStartTile;
            MaxNumTroopsInTile = maxNumTroopsInTile;
            MaxNumPlayersInGame = maxNumPlayersInGame;
        }

        public int BoardSize { get; }
        public int NumTroopsInStartTile { get; }
        public int MaxNumTroopsInTile { get; }
        public int MaxNumPlayersInGame { get; }

        public static readonly RuleSet Default = new RuleSet(boardSize: 6, numTroopsInStartTile:2, maxNumTroopsInTile:5, maxNumPlayersInGame:4);
    }
}