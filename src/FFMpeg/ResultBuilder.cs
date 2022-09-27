using FFMpeg.Abstractions;

namespace FFMpeg
{
    public class ResultBuilder : IResultBuilder
    {
        public ResultBuilder(string outputFile, AudioModel outputModel)
        {
            OutputFile = outputFile ?? throw new ArgumentNullException(nameof(outputFile));
            OutputModel = outputModel ?? throw new ArgumentNullException(nameof(outputModel));
        }

        /// <summary>
        /// Results of the analysed audio file. 
        /// </summary>
        public AudioModel? ResultModel { get; set; }
        /// <summary>
        /// Path to write the normalised audio file. 
        /// </summary>
        public string OutputFile { get; set; }
        /// <summary>
        /// Model that defines the output levels.
        /// </summary>
        public AudioModel OutputModel { get; set; }
    }
}