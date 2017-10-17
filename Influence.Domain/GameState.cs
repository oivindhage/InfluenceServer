namespace Influence.Domain
{
    public class GameState
    {
        public Player CurrentPlayer { get; set; }
        public PlayerPhaseEnum PlayerPlayerPhase { get; set; }
        public GamePhaseEnum GamePhase { get; set; }
    }
}