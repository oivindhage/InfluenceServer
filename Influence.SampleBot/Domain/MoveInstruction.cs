namespace Influence.SampleBot.Domain
{
    public class MoveInstruction
    {
        public int SourceTileId { get; set; }
        public int DestTileId { get; set; }

        public MoveInstruction(int sourceTileId, int destTileId)
        {
            SourceTileId = sourceTileId;
            DestTileId = destTileId;
        }
    }
}