using System.Collections.Generic;

public class Song {
    internal Song() {
        List<MeasureElement> MeasureElements = new List<MeasureElement>();
    }

    public List<MeasureElement> MeasureElements { get; internal set; }
}