using System.Diagnostics;

namespace FFMpeg.Abstractions
{
    public interface IStartInfoProvider
    {
        ProcessStartInfo GetStartInfo(string arguments);

    }
}