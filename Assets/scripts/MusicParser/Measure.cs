using System.Collections.Generic;
public class Measure {
    internal Measure (){
        Width = -1;
        Number = 0;
        MeasureElements = new List<MeasureElement>();
    }

    public decimal Width { get; internal set; }
    public List<MeasureElement> MeasureElements { get; internal set; }

    public int Number { get; internal set; }
}
