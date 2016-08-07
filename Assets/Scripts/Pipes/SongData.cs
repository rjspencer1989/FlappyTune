using System;
using System.Collections.Generic;

[Serializable]
public class SongData{
    public List<Pipe> Pipes { get; set; }
    public float Tempo { get; set; }
    public string BeatType { get; set; }

    List<string> NotesUsed { get; set; }

    public SongData(){
        Pipes = new List<Pipe>();
    }
}
