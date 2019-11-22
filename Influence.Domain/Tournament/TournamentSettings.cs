using System;

namespace Influence.Domain.Tournament
{
    public class TournamentSettings
    {
        public int MinNumPlayersInEachGame { get; set; } = 2;
        public int MaxNumPlayersInEachGame { get; set; } = 3;

        public TournamentSettings Proper()
        {
            MinNumPlayersInEachGame = Math.Max(2, MinNumPlayersInEachGame);
            MaxNumPlayersInEachGame = Math.Max(MaxNumPlayersInEachGame, MinNumPlayersInEachGame);
            return this;
        }
    }
}