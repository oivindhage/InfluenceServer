﻿using System.Collections.Generic;

namespace Influence.Domain
{
    public class Participant
    {
        public Player Player { get; set; }
        public List<Tile> OwnedTiles { get; set; } = new List<Tile>();
        public bool IsAlive { get; set; }
        public int Rank { get; set; }

        public Participant()
        { }

        public Participant(Player player)
        {
            Player = player;
            IsAlive = true;
        }
    }
}