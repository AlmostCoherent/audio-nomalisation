namespace FFMpeg.Abstractions
{
    public interface IResultBuilder
    {
        string OutputFile { get; set; }
        AudioModel OutputModel { get; set; }
        AudioModel? ResultModel { get; set; }
    }
}