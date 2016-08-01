using System;
using UnityEngine;

[Serializable]
public class Pipe : ScriptableObject{
    public char Step { get; set; }
    public int Octave { get; set; }
    public int Alter { get; set; }
    public string Type { get; set; }
    public string BeatType { get; set; }
    public int Tempo { get; set; }
}
