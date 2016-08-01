namespace MusicParser{
    public class DirectionType{
        internal DirectionType(){
            MetronomeMark = new Metronome();
        }

        public Metronome MetronomeMark { get; internal set; }
    }
}
