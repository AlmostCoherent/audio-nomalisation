//using Microsoft.Extensions.Logging;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Diagnostics;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace FFMpeg
//{
//    internal class ProcessingItem
//    {
//        public class ProcessingItem
//        {
//            private const int SUCCESS_EXIT_CODE = 0x00000000;

//            private readonly ILogger logger;
//            private readonly bool overwrite;
//            private readonly CancellationToken token;
//            private Process? process;

//            public ProcessingItem(ILogger logger, ProcessingItemInfo pInfo, IResultBuilder builder, NormalizationPass pass, CancellationToken token, bool overwrite = true)
//            {
//                this.process = pInfo.Process;
//                Pass = pass;
//                this.token = token;
//                this.overwrite = overwrite;
//                this.logger = logger;
//                this.resultBuilder = builder;
//                InputFileName = pInfo.InputFile;
//                OutputFileName = pInfo.OutputFile;
//                Result = "";

//                var hasFileName = !string.IsNullOrEmpty(process.StartInfo.FileName);
//                Debug.Assert(hasFileName, "Filename is expected to be provided to the process.");
//                if (!hasFileName)
//                {
//                    logger.LogError($"No executable file name is provided for processing file '{InputFileName}'");
//                    CleanUp();
//                    return;
//                }

//                var hasArguments = !string.IsNullOrWhiteSpace(process.StartInfo.Arguments);
//                Debug.Assert(hasArguments, "Arguments is expected to be provided to the process");
//                if (!hasArguments)
//                {
//                    logger.LogError($"No argument is provided for processing file '{InputFileName}'.");
//                    CleanUp();
//                    return;
//                }

//                Initialize();
//                Start();
//            }

//            public event EventHandler<StatusChangedEventArgs>? StatusChanged;

//            public NormalizationResult Result { get; private set; }

//            public string InputFileName { get; }

//            public string OutputFileName { get; }

//            public NormalizationPass Pass { get; }

//            public string? ErrorMessage { get; private set; }

//            public ProcessingItemStatus Status { get; private set; }

//            public void Start()
//            {
//                if (process == null || token.IsCancellationRequested)
//                    return;

//                var status =
//                    (Pass == NormalizationPass.One)
//                    ? ProcessingItemStatus.PassOneStarted
//                    : ProcessingItemStatus.PassTwoStarted;

//                SetStatus(status);

//                try
//                {
//                    process.Start();
//                    process.BeginOutputReadLine();
//                    process.BeginErrorReadLine();
//                    logger.LogInformation($"Started processing... (File: '{Path.GetFileName(InputFileName)}', Pass: '{Pass}', '{process.StartInfo.FileName} {process.StartInfo.Arguments}'");
//                }
//                catch (Win32Exception wEx)
//                {
//                    var msg = $"An error occured while opening file '{process.StartInfo.FileName}'";
//                    logger.LogError(wEx, msg);
//                    ErrorMessage = msg;
//                    SetStatus(ProcessingItemStatus.Faulted);
//                    CleanUp();

//                }
//                catch (Exception ex)
//                {
//                    Debug.Fail(ex.Message);
//                    var msg = $"Something unexpected happened while starting the process for file '{InputFileName}'.";
//                    logger.LogError(ex, msg);
//                    ErrorMessage = msg;
//                    SetStatus(ProcessingItemStatus.Faulted);
//                    CleanUp();
//                    throw;
//                }
//            }

//            private void Initialize()
//            {
//#pragma warning disable CS8602 // Dereference of a possibly null reference.
//                process.StartInfo.UseShellExecute = false;
//                process.StartInfo.RedirectStandardOutput = true;
//                process.StartInfo.RedirectStandardInput = true;
//#pragma warning restore CS8602 // Dereference of a possibly null reference.

//                Subscribe();
//            }

//            private void CleanUp()
//            {
//                Unsubscribe();
//                process?.Dispose();
//                process = null;
//                resultBuilder = null;
//            }

//            private void SetStatus(ProcessingItemStatus status)
//            {
//                Status = status;
//                StatusChanged?.Invoke(this, new StatusChangedEventArgs(status, this));
//            }
//            private void Subscribe()
//            {
//#pragma warning disable CS8602 // Dereference of a possibly null reference.
//                process.OutputDataReceived += OnProcessOutputReceived;
//                process.ErrorDataReceived += OnProcessErrorReceived;
//                process.Exited += OnProcessExited;
//#pragma warning restore CS8602 // Dereference of a possibly null reference.
//            }

//            private void Unsubscribe()
//            {
//#pragma warning disable CS8602 // Dereference of a possibly null reference.
//                process.OutputDataReceived -= OnProcessOutputReceived;
//                process.ErrorDataReceived -= OnProcessErrorReceived;
//                process.Exited -= OnProcessExited;
//#pragma warning restore CS8602 // Dereference of a possibly null reference.
//            }

//            private void OnProcessExited(object? sender, EventArgs e)
//            {
//                if (process?.ExitCode == SUCCESS_EXIT_CODE)
//                {
//                    var status =
//                        (Pass == NormalizationPass.One)
//                        ? ProcessingItemStatus.PassOneFinished
//                        : ProcessingItemStatus.PassTwoFinished;

//                    SetStatus(status);

//                    try
//                    {

//                    }
//                    catch (InvalidOperationException invEx)
//                    {
//                        var msg = $"Could not build result for file '{InputFileName}' in pass '{Pass}'";
//                        logger.LogError(invEx, msg);
//                        ErrorMessage = msg;
//                    }

//                    Result = resultBuilder?.Build() ?? NormalizationResult.Empty;
//                }
//                else
//                {
//                    SetStatus(ProcessingItemStatus.Faulted);
//                }

//                CleanUp();
//            }

//            private void OnProcessErrorReceived(object sender, DataReceivedEventArgs e)
//            {
//                SetStatus(ProcessingItemStatus.Faulted);
//            }

//            private void OnProcessOutputReceived(object sender, DataReceivedEventArgs e)
//            {
//                if (!string.IsNullOrWhiteSpace(e.Data) && resultBuilder != null)
//                {
//                    resultBuilder.Add(e.Data);
//                }
//            }
//        }
//    }
//}
