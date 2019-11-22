using System;

namespace Influence.Domain
{
    public class Player
    {
        public Player() { }

        public Player(Guid id, string name, string colorRgbCsv, bool isStandInOnly = false)
        {
            Id = id;
            Name = name;
            ColorRgbCsv = colorRgbCsv;
            IsStandInOnly = isStandInOnly;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ColorRgbCsv { get; set; }

        public int Score { get; set; }
        public int NumPos1 { get; set; }
        public int NumPos2 { get; set; }
        public int NumPos3 { get; set; }
        public int NumPos4 { get; set; }

        public int NumAvailableReinforcements { get; set; }

        // Helping out in a game / not his primary game
        public bool IsStandInOnly { get; set; }
    }
}