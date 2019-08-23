using Influence.Domain;

namespace Influence.SampleBot.Domain
{
    public class MoveInstruction
    {
        public Tile SourceTile { get; }
        public Tile DestinationTile { get; }

        public MoveInstruction(Tile sourceTile, Tile destinationTile)
        {
            SourceTile = sourceTile;
            DestinationTile = destinationTile;
        }
    }
}