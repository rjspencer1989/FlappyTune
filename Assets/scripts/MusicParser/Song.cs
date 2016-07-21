using System.Collections.Generic;

public class Song {
    internal Song() {
        MeasureElements = new List<MeasureElement>();
    }

    public List<MeasureElement> MeasureElements { get; internal set; }
}