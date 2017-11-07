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

        public int BoardSize { get; set; }
        public int NumTroopsInStartTile { get; set; }
        public int MaxNumTroopsInTile { get; set; }
        public int MaxNumPlayersInGame { get; set; }

        public static readonly RuleSet Default = new RuleSet(boardSize: 6, numTroopsInStartTile: 2, maxNumTroopsInTile: 5, maxNumPlayersInGame: 4);
    }
}