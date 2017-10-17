﻿namespace Influence.Domain
{
    public class GameState
    {
        public Player CurrentPlayer { get; set; }
        public string PlayerPhase { get; set; }
        public string GamePhase { get; set; }

        public GameState()
        {
            CurrentPlayer = null;
            PlayerPhase = Consts.PlayerPhase.Undefined;
            GamePhase = Consts.GamePhase.NotStarted;
        }
    }
}