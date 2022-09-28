namespace Files.Abstractions
{
    public interface IBaseFileConfig
    {
        public string OutputPath { get; set; }
        public string OutputFileName { get; set; }
        public string InputPath { get; set; }
        public string InputFileName { get; set; }
    }
}
