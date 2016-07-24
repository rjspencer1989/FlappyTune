using System.Collections.Generic;

public class Song {
    internal Song() {
        Measures = new List<Measure>();
    }

    public List<Measure> Measures { get; internal set; }
}
