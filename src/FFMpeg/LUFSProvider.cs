using FFMpeg.Abstractions;
using Microsoft.Extensions.Logging;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace FFMpeg
{
    public class LUFSProvider : ILUFSProvider
    {
        private readonly IExecuteProcess _executeProcess;
        private ResultBuilder _resultBuilder;
        String strError;


        /// <summary>
        /// Constructor for <see cref="LUFSProvider"/>.
        /// </summary>
        /// <param name="executeProcess"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public LUFSProvider(IExecuteProcess executeProcess)
        {
            _executeProcess = executeProcess ?? throw new ArgumentNullException(nameof(executeProcess));
        }

        public string AdjustLufsOfInput(string inputFile)
        {
            var file = new FileInfo(inputFile);
            var resultPath = Path.Combine(file.DirectoryName ?? "", file.Name.Replace((file.Extension), "").Replace(" ", "") + "-192kpbs.mp3");
            _resultBuilder = new ResultBuilder(
                resultPath,
                new AudioModel
                {
                    Integrated = "-9",
                    TruePeak = "-1.5",
                    LRA = "11"
                });

            var firstPassArgs = $" -i \"{file.FullName}\" -af loudnorm=print_format=summary -f null -";
            var firstPassResult = _executeProcess.Run(firstPassArgs);

            _resultBuilder.ResultModel = firstPassResult;

            var secondPassArguments = $" -i \"{file.FullName}\"  -ab 192k -af loudnorm=I={_resultBuilder.OutputModel.Integrated}:LRA={_resultBuilder.OutputModel.LRA}:measured_I={_resultBuilder.ResultModel.Integrated}:measured_TP={_resultBuilder.ResultModel.TruePeak}:measured_LRA={_resultBuilder.ResultModel.LRA}:measured_thresh={_resultBuilder.ResultModel.Threshold}:linear=true:print_format=summary  -ar 48k {_resultBuilder.OutputFile}";
            var secondPassResult = _executeProcess.Run(secondPassArguments);

            return resultPath;
        }
    }
}