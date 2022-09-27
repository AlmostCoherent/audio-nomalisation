﻿using FFMpeg.Abstractions;
using Microsoft.Extensions.Logging;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace FFMpeg
{
    public class LUFSProvider
    {
        private readonly IExecuteProcess _executeProcess;
        private ResultBuilder _resultBuilder;
        String strError;


        public NormalizationResult Result { get; private set; }
        /// <summary>
        /// Constructor for <see cref="LUFSProvider"/>.
        /// </summary>
        /// <param name="executeProcess"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public LUFSProvider(IExecuteProcess executeProcess)
        {
            _executeProcess = executeProcess ?? throw new ArgumentNullException(nameof(executeProcess));
        }

        public void GetMeLUFS(string inputFile)
        {
            var file = new FileInfo(inputFile);
            _resultBuilder = new ResultBuilder(
                file.Directory!.FullName + "\\processed." + file.Extension.ToLower(),
                new AudioModel
                {
                    Integrated = "-16",
                    TruePeak = "-1.5",
                    LRA = "15"
                });

            var firstPassArgs = $" -i \"{file.FullName}\" -af loudnorm=I={_resultBuilder.OutputModel.Integrated}:TP={_resultBuilder.OutputModel.TruePeak}:LRA={_resultBuilder.OutputModel.LRA}:print_format=summary -f null -";
            var firstPassResult = _executeProcess.Run(firstPassArgs);

            _resultBuilder.ResultModel = firstPassResult;

            var secondPassArguments = $" -i \"{file.FullName}\" -af loudnorm=I={_resultBuilder.OutputModel.Integrated}:TP={_resultBuilder.OutputModel.TruePeak}:LRA={_resultBuilder.OutputModel.LRA}:measured_I={_resultBuilder.ResultModel.Integrated}:measured_TP={_resultBuilder.ResultModel.TruePeak}:measured_LRA={_resultBuilder.ResultModel.LRA}:measured_thresh={_resultBuilder.ResultModel.Threshold}:linear=true:print_format=summary  -ar 48000 {_resultBuilder.OutputFile}";
            var secondPassResult = _executeProcess.Run(secondPassArguments);
        }
    }
}