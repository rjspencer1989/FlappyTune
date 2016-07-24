using System.Collections.Generic;

public class Measure{
    internal Measure(){
        MeasureAttributes = new List<MeasureAttribute>();
        Width = 0;
        Number = 0;
        Direction = new DirectionElement();
        MeasureElements = new List<MeasureElement>();
    }

    public List<MeasureAttribute> MeasureAttributes { get; internal set; }
    public DirectionElement Direction { get; internal set; }
    public decimal Width { get; internal set; }
    public int Number { get; internal set; }
    public List<MeasureElement> MeasureElements { get; internal set; }
}
