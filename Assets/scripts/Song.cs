using System;
[Serializable]
public class Song{
    public ScorePartwise score_partwise;
}

[Serializable]
public class ScorePartwise
{
    public Part part;
}

[Serializable]
public class Part
{
    public Measure[] measure;
}

[Serializable]
public class Measure
{
    public Note[] note;
}

[Serializable]
public class Note
{
    public Pitch pitch;
    public string step;
    public int duration;
    public string type;
    public string dot;
}

[Serializable]
public class Pitch{
    public string step;
    public int octave;
}