class Pitch{
    string step;
    int octave;

    public Pitch(string step, int octave){
        this.step = step;
        this.octave = octave;
    }

    public override string ToString(){
        return this.step + this.octave.ToString();
    }
}