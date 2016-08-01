namespace MusicParser{
    public enum TimeSymbol{
        Normal, Common, Cut, SingleNumber
    }

    public class TimeSignature{
        internal TimeSignature()
        {
            Beats = 0;
            Mode = string.Empty;

        }

        public int Beats { get; internal set; }
        public string Mode { get; internal set; }
        public TimeSymbol Symbol { get; internal set; }
    }
}
