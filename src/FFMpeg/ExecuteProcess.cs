using FFMpeg.Abstractions;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace FFMpeg
{
    public class ExecuteProcess : IExecuteProcess
    {
        private IStartInfoProvider _startInfoProvider;
        private readonly ILogger<ExecuteProcess> _logger;

        /// <summary>
        /// Constructs the ExecuteProcess class which runs a process that analyses an audio file using the arguments provided. This uses the <see cref="IStartInfoProvider"/> which provides the StartInfo for the process.
        /// </summary>
        /// <param name="startInfoProvider"></param>
        /// <param name="logger"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public ExecuteProcess(IStartInfoProvider startInfoProvider, ILogger<ExecuteProcess> logger)
        {
            _startInfoProvider = startInfoProvider ?? throw new ArgumentNullException(nameof(startInfoProvider));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Runs an FFMPEG process using the specified arguments and returns the results. 
        /// </summary>
        /// <param name="arguments"></param>
        /// <returns></returns>
        public AudioModel Run(string arguments)
        {
            var returnModel = new AudioModel();
            using (var process = new Process())
            {
                _logger.LogInformation("Process setup start.");
                process.StartInfo = _startInfoProvider.GetStartInfo(arguments);
                process.ErrorDataReceived += (s, e) =>
                {
                    string strLine = e.Data;
                    if (!string.IsNullOrEmpty(strLine))
                    {
                        _logger.LogInformation(strLine);
                        if (strLine.StartsWith("Input Integrated:"))
                        {
                            returnModel.Integrated = strLine.ExtractNumberFromFFMPEGLine();
                            _logger.LogInformation(string.Format("Integrated result: ", returnModel.Integrated));
                        }
                        else if (strLine.StartsWith("Input True"))
                        {
                            returnModel.TruePeak = strLine.ExtractNumberFromFFMPEGLine();
                            _logger.LogInformation(string.Format("TruePeak result: ", returnModel.TruePeak));
                        }
                        else if (strLine.StartsWith("Input LRA:"))
                        {
                            returnModel.LRA = strLine.ExtractNumberFromFFMPEGLine();
                            _logger.LogInformation(string.Format("LRA result: ", returnModel.LRA));
                        }
                        else if (strLine.StartsWith("Input Threshold:"))
                        {
                            returnModel.Threshold = strLine.ExtractNumberFromFFMPEGLine();
                            _logger.LogInformation(string.Format("Threshold result: ", returnModel.Threshold));
                        }
                        else if (strLine.StartsWith("Target"))
                        {
                            returnModel.Offset = strLine.ExtractNumberFromFFMPEGLine();
                            _logger.LogInformation(string.Format("Offset result: ", returnModel.Offset));
                        }
                    }
                };
                _logger.LogInformation("Process setup end.");

                try
                {
                    _logger.LogInformation("Processing start.");
                    process.Start();

                    process.BeginOutputReadLine();

                    process.BeginErrorReadLine();

                    process.WaitForExit();
                    _logger.LogInformation("Processing end.");
                }
                catch (Exception ex)
                {
                    _logger.LogError("Processing failed.", ex.Message);
                    throw;
                }
            }
            return returnModel;
        }
    }
}
