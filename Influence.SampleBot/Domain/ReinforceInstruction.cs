namespace Influence.SampleBot.Domain
{
    public class ReinforceInstruction
    {
        public int TileId { get; }

        public ReinforceInstruction(int tileId)
        {
            TileId = tileId;
        }
    }
}