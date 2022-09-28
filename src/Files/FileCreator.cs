using Files.Abstractions;

namespace Files
{
    public class FileCreator : IFileCreator
    {
        private readonly IBaseFileConfig _baseFileConfig;

        public FileCreator(IBaseFileConfig baseFileConfig)
        {
            _baseFileConfig = baseFileConfig ?? throw new ArgumentNullException(nameof(baseFileConfig));
        }

        public async Task<string> CreateFromStream(Stream formFile)
        {
            using (var fileStream = File.Create(Path.Combine(_baseFileConfig.InputPath, _baseFileConfig.InputFileName)))
            {
                formFile.Seek(0, SeekOrigin.Begin);
                formFile.CopyTo(fileStream);
            }
            return _baseFileConfig.OutputPath;
        }
    }
}