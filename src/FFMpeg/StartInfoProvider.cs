using FFMpeg.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFMpeg
{
    public class StartInfoProvider : IStartInfoProvider
    {
        private ProcessStartInfo FFMpegStartInfo;

        public StartInfoProvider()
        {
            FFMpegStartInfo = new ProcessStartInfo()
            {
                FileName = @"D:\ffmpeg-2022-09-19-git-4ba68639ca-full_build\bin\ffmpeg",
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,

            };
        }

        public ProcessStartInfo GetStartInfo(string arguments)
        {
            FFMpegStartInfo.Arguments = arguments;
            return FFMpegStartInfo;
        }
    }
}
