using Files.Abstractions;

namespace Files
{
    public class BaseFileConfig : IBaseFileConfig
    {
        private string outputPath = AppContext.BaseDirectory;

        public string OutputPath {
            get {
                return outputPath; } 
            set {
                outputPath= value; } 
        }

        private string outputFileName = Guid.NewGuid().ToString("N");
        public string OutputFileName { 
            get { 
                return outputFileName; } 
            set { 
                outputFileName = value; } 
        }

        private string inputPath = AppContext.BaseDirectory;

        public string InputPath
        {
            get
            {
                return inputPath;
            }
            set
            {
                outputPath = value;
            }
        }
        public string? InputFileName { get; set; }
    }
}
