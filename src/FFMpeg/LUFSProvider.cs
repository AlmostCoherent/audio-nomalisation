using Microsoft.Extensions.Logging;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;

namespace FFMpeg
{
    public class LUFSProvider
    {
        private const int SUCCESS_EXIT_CODE = 0x00000000;
        private readonly ILogger logger;
        private IResultBuilder? resultBuilder = new ResultBuilder();
        private Process _process;

        public NormalizationResult Result { get; private set; }
        public LUFSProvider(ILogger logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task GetMeLUFS(string inputFile)
        {
            string? args = null;
            try
            {
                var startInfo = new ProcessStartInfo()
                {
                    FileName = @"D:\ffmpeg-2022-09-19-git-4ba68639ca-full_build\bin\ffmpeg.exe",
                    //Getting just the version works.  
                    //Arguments = "-version",
                    //Starting to calculate loudness does not. 
                    Arguments = $" -i '{inputFile}' -af loudnorm=print_format=summary -f null -",
                    CreateNoWindow = false,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardInput = true,
                    RedirectStandardError = false,
                };

                _process = new Process { StartInfo = startInfo };
                SetupSubscriptions();

                try
                {
                    _process.Start();
                    _process.BeginOutputReadLine();
                    _process.BeginErrorReadLine();
                    _process.WaitForExit();
                    logger.LogInformation($"Started processing: (File:- '{inputFile}', '{_process.StartInfo.FileName} {_process.StartInfo.Arguments}'");
                }
                catch (Win32Exception wEx)
                {
                    var msg = $"An error occured while opening file '{_process.StartInfo.FileName}'";

                }
                catch (Exception ex)
                {
                    Debug.Fail(ex.Message);
                    throw;
                }

            }
            catch (Win32Exception wex) {
                throw;
            } catch (Exception ex) {
                throw;
            }
        }

        private void SetupSubscriptions()
        {
            _process.OutputDataReceived += OnProcessOutputReceived;
            _process.ErrorDataReceived += OnProcessErrorReceived;
            _process.Exited += OnProcessExited;
        }

        private void OnProcessExited(object? sender, EventArgs e)
        {
            Result = resultBuilder?.Build() ?? NormalizationResult.Empty;
        }

        private void OnProcessErrorReceived(object sender, DataReceivedEventArgs e)
        {
            var poo = "";
        }

        private void OnProcessOutputReceived(object sender, DataReceivedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(e.Data) && resultBuilder != null)
            {
                resultBuilder.Add(e.Data);
            }
        }

    }
    public enum ProcessingItemStatus
    {
        None,
        Faulted,
        PassOneStarted,
        PassOneFinished,
        PassTwoStarted,
        PassTwoFinished
    }

}