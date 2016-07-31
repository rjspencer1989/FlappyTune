namespace MusicParser{
    public class MeasureElement {
        public MeasureElementType Type { get; set; }
        public object Element { get; set; }
    }

    public enum MeasureElementType {
        Note,
        Backup,
        Forward
    }    
}
