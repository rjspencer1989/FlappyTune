using System;
using System.Collections.Generic;

public class Pitch {
    internal Pitch(){
        Alter = 0;
        Octave = 0;
        Step = new Char(); 
    }

    public int Alter { get; internal set; }
    public int Octave { get; internal set; }
    public char Step { get; internal set; }

    public override string ToString(){
        string adj = "natural";
        if(Alter > 0){
            adj = "sharp";
        } else if(Alter < 0){
            adj = "flat";
        }
        return string.Format("{0} {1} {2}", Step, adj, Octave);
    }

    public override bool Equals (Object obj)
    {   
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        Pitch p = obj as Pitch;
        if((Object)p == null){
            return false;
        }

        // TODO: write your implementation of Equals() here
        return (this.Step == p.Step) && (this.Octave == p.Octave) && (this.Alter == p.Alter);
    }

    public static bool operator ==(Pitch a, Pitch b){
        // If both are null, or both are same instance, return true.
        if (System.Object.ReferenceEquals(a, b))
        {
            return true;
        }

        // If one is null, but not both, return false.
        if (((object)a == null) || ((object)b == null))
        {
            return false;
        }

        return (a.Alter == b.Alter) && (a.Octave == b.Octave) && (a.Step == b.Step);
    }

    public static bool operator !=(Pitch a, Pitch b){
        return !(a == b);
    }
    
    // override object.GetHashCode
    public override int GetHashCode()
    {
        // TODO: write your implementation of GetHashCode() here
        return Alter ^ Octave;
    }

    public double getPitchOffset(Pitch other){
        Dictionary<char, int> stepOrder = PitchComparer.getStepOrder();
        double thisAlter = this.getAlter();
        double otherAlter = other.getAlter();
        int octaveOffset = this.Octave - other.Octave;
        double alterOffset = thisAlter - otherAlter;
        int stepOffset = stepOrder[this.Step] - stepOrder[other.Step];
        return stepOffset + (8 * octaveOffset) + alterOffset;
    }

    private double getAlter(){
        double alter = 0;
        if(this.Alter > 0){
            alter = Math.Pow(0.5, this.Alter);
        } else if(this.Alter < 0){
            alter = -1 * (Math.Pow(0.5, Math.Abs(this.Alter)));
        }
        return alter;
    }
}
