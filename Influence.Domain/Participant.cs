namespace Influence.Domain
{
    public class Participant
    {
        public Player Player { get; set; }
        public bool IsAlive { get; set; }
        public int Rank { get; set; }

        public Participant() { }

        public Participant(Player player)
        {
            Player = player;
            IsAlive = true;
        }
    }
}