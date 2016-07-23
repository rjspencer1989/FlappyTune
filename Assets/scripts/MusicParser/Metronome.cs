public class Metronome
{
    internal Metronome()
    {
        BeatUnit = string.Empty;
        PerMinute = 0;
    }

    public string BeatUnit { get; internal set; }
    public int PerMinute { get; internal set; }
}
