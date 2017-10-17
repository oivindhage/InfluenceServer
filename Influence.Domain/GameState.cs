namespace Influence.Domain
{
    public class GameState
    {
        public Player CurrentPlayer { get; set; }
        public PlayerPhaseEnum PlayerPlayerPhase { get; set; }
        public GamePhaseEnum GamePhase { get; set; }

        public GameState()
        {
            CurrentPlayer = null;
            PlayerPlayerPhase = PlayerPhaseEnum.Undefined;
            GamePhase = GamePhaseEnum.NotStarted;
        }
    }
}