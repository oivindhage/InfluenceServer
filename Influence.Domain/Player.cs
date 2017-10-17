using System;

namespace Influence.Domain
{
    public class Player
    {
        public Player(Guid id, string nick, string colorRgb)
        {
            Id = id;
            Nick = nick;
            ColorRgb = colorRgb;
        }

        public Guid Id { get; }
        public string Nick { get; }
        public string ColorRgb { get; set; }

        public int Score { get; set; }
        public int NumPos1 { get; set; }
        public int NumPos2 { get; set; }
        public int NumPos3 { get; set; }
        public int NumPos4 { get; set; }
    }
}