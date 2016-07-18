using System.Collections.Generic;

public class Score {
    internal Score() {
        Parts = new List<Part>();
    }

    public List<Part> Parts { get; internal set; }
}