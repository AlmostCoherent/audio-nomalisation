namespace FFMpeg.Abstractions
{
    /// <summary>
    /// Model for the analysed file including: Integrated, True Peak, LRA, Threshhold.
    /// </summary>
    public class AudioModel
    {
        public string? Integrated { get; set; }
        public string? TruePeak { get; set; }
        public string? LRA { get; set; }
        public string? Threshold { get; set; }
        public string? Offset { get; set; }
    }
}
