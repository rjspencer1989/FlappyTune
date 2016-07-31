using System.Collections.Generic;
namespace MusicParser{
    public class Measure{
        internal Measure(){
            Width = -1;
            Number = 0;
            Direction = new DirectionElement();
            MeasureElements = new List<MeasureElement>();
        }

        public MeasureAttribute MeasureAttributes { get; internal set; }
        public DirectionElement Direction { get; internal set; }
        public decimal Width { get; internal set; }
        public int Number { get; internal set; }
        public List<MeasureElement> MeasureElements { get; internal set; }
    }
}
