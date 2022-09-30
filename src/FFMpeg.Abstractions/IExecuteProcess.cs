namespace FFMpeg.Abstractions
{
    public interface IExecuteProcess
    {
        AudioModel Run(string arguments);
    }
}