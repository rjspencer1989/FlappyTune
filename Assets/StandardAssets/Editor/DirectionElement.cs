namespace MusicParser{
    public class DirectionElement{
        internal DirectionElement(){
            Type = new DirectionType();
            Sound = new SoundElement();
        }

        public SoundElement Sound { get; internal set; }
        public DirectionType Type { get; internal set; }
    }
}
