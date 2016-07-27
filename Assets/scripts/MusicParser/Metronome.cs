public class Metronome
{
    internal Metronome()
    {
        BeatUnit = string.Empty;
        PerMinute = 0.0f;
    }

    public string BeatUnit { get; internal set; }
    public float PerMinute { get; internal set; }
}
