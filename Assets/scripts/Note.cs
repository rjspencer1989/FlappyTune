using UnityEngine;

class Note{
    Pitch pitch;

    string noteType;
    int duration;

    public Note(string step, int octave, string type, int duration){
        this.pitch = new Pitch(step, octave);
        this.noteType = type;
        this.duration = duration;
    }

    public override string ToString(){
        return this.pitch.ToString() + " " + noteType;
    }
}