using System;

[Serializable]
public class Pipe{
    public char Step { get; set; }
    public int Octave { get; set; }
    public int Alter { get; set; }
    public string Type { get; set; }
    public string BeatType { get; set; }
    public float Tempo { get; set; }
}
