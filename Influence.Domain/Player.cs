using System;

namespace Influence.Domain
{
    public class Player
    {
        public Player(Guid id, string nick, string colorRgbCsv)
        {
            Id = id;
            Nick = nick;
            ColorRgbCsv = colorRgbCsv;
        }

        public Guid Id { get; }
        public string Nick { get; }
        public string ColorRgbCsv { get; set; }

        public int Score { get; set; }
        public int NumPos1 { get; set; }
        public int NumPos2 { get; set; }
        public int NumPos3 { get; set; }
        public int NumPos4 { get; set; }
    }
}