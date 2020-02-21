using System.Collections.Generic;

namespace Influence.Domain
{
    public class GameEventHistory
    {
        public List<EventBatch> EventBatches { get; } = new List<EventBatch>();
        
        public class EventBatch
        {
            public List<CellChangedEvent> Events { get; } = new List<CellChangedEvent>();
        } 
        
        public class CellChangedEvent
        {
            public int X { get; }
            public int Y { get; }
            public int NumTroops { get; }
            public string OwnerColorRgbCsv { get; }
            
            
            public CellChangedEvent(int x, int y, int numTroops, string ownerColorRgbCsv)
            {
                X = x;
                Y = y;
                NumTroops = numTroops;
                OwnerColorRgbCsv = ownerColorRgbCsv;
            }
        }
    }
}