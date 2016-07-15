using System;
using System.Collections.Generic;
using UnityEngine;

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
    List<string> steps = new List<string>(new string[]{"C", "D", "E", "F", "G", "A", "B"});
    public string step;
    public int octave;

    public int getPitchValue(){
        return (this.getStepNum() + 1) * this.octave;
    }

    public int getStepNum(){
        return steps.IndexOf(this.step);
    }

    public int getPitchOffset(Pitch other){
        return ((this.getStepNum() + 1 - (steps.IndexOf(other.step) + 1)) + (8 * (this.octave - other.octave)));
    }

    public override string ToString(){
        return String.Format("{0}:{1}", this.step, this.octave);
    }
}
