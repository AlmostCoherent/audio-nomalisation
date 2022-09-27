using FFMpeg.Abstractions;

namespace FFMpeg
{
    public interface IExecuteProcess
    {
        AudioModel Run(string arguments);
    }
}