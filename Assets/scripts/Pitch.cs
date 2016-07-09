using System;

class Pitch{
    string step;
    int octave;

    private enum stepEnum
    {
        rest, c, d, e, f, g, a, b
    }

    public Pitch(string step, int octave){
        this.step = step;
        this.octave = octave;
    }

    public override string ToString(){
        return this.step + this.octave.ToString();
    }

    public int getPitchVal(){
        stepEnum stepVal = (stepEnum)Enum.Parse(typeof(stepEnum), this.step, true);
        return (int)stepVal * octave;
    }

    public int getPitchOffset(Pitch other){
        stepEnum stepVal = (stepEnum)Enum.Parse(typeof(stepEnum), this.step, true);
        stepEnum otherStepVal = (stepEnum)Enum.Parse(typeof(stepEnum), other.step, true);
        if(other.octave == this.octave){
            return stepVal - otherStepVal;
        }
        return (Math.Abs(stepVal - otherStepVal)) + (8 * (Math.Abs(this.octave - other.octave)));
    }
}