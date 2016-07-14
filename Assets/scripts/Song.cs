using System;

[Serializable]
public class Song{
    public Note[] notes;
}

[Serializable]
public class Note{
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
