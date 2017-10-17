using System;

namespace Influence.Domain
{
    public class Player
    {
        public Player(Guid id, string name, string colorRgbCsv)
        {
            Id = id;
            Name = name;
            ColorRgbCsv = colorRgbCsv;
        }

        public Guid Id { get; }
        public string Name { get; }
        public string ColorRgbCsv { get; set; }

        public int Score { get; set; }
        public int NumPos1 { get; set; }
        public int NumPos2 { get; set; }
        public int NumPos3 { get; set; }
        public int NumPos4 { get; set; }

        public int NumAvailableReinforcements { get; set; }
    }
}