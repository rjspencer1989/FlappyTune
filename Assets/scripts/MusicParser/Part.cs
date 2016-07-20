using System.Collections.Generic;

public class Part {
    internal Part(){
        Id = string.Empty;
        Measures = new List<Measure>();
    }

    public string Id { get; internal set; }
    public List<Measure> Measures { get; internal set; }
}
