using Influence.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Influence.GameClient.Mock
{
    public class DummyService
    {
        static Random random = new Random();

        public static Session GetDummySession()
        {
            Session session = new Session();
            session.Board = GetDummyBoard();
            session.GameState = new GameState
            {
                CurrentPlayer = null,
                PlayerPlayerPhase = PlayerPhaseEnum.Undefined,
                GamePhase = GamePhaseEnum.Waiting
            };
            return session;
        }

        public static Board GetDummyBoard()
        {
            Board board = new Board();
            board.TileRows = new List<TileRow>();
            for (int r = 0;r < 6; ++r)
            {
                TileRow tileRow = CreateRandomTileRow(r);
                board.TileRows.Add(tileRow);
            }
            AddPlayers(board);
            return board;
        }

        private static void AddPlayers(Board board)
        {
            for (int i = 0; i < 4;)
            {
                var tileRow = board.TileRows.OrderBy(x => random.NextDouble()).First();
                var tile = tileRow.Tiles.OrderBy(x => random.NextDouble()).First();
                if (tile.NumTroops == 0)
                {
                    tile.OwnerNick = $"Player {i}";
                    tile.OwnerId = Guid.NewGuid();
                    tile.NumTroops = 2;
                    ++i;
                }
            }
        }

        private static TileRow CreateRandomTileRow(int rowId)
        {
            var tileRow = new TileRow();
            tileRow.Tiles = new List<Tile>();
            for (int t = 0;t < 6; ++t)
            {
                Tile tile = new Tile(t, rowId, 6);
                tileRow.Tiles.Add(tile);
            }
            return tileRow;
        }
    }
}
