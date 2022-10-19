using Files.Abstractions;
using Microsoft.Extensions.Logging;

namespace Files
{
    public class FileCreator : IFileCreator
    {
        private readonly IBaseFileConfig _baseFileConfig;
        private readonly ILogger<FileCreator> _logger;

        public FileCreator(IBaseFileConfig baseFileConfig, ILogger<FileCreator> logger)
        {
            _baseFileConfig = baseFileConfig ?? throw new ArgumentNullException(nameof(baseFileConfig));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public string CreateFromStream(Stream formFile, string fileName, string? path = default)
        {
            var fileStoreLocation = Path.Combine((path ?? AppContext.BaseDirectory), fileName);
            _logger.LogInformation($"File stored location: { fileStoreLocation }");
            _logger.LogInformation($"Writing file.");
            try
            {
                using (var fileStream = File.Create(fileStoreLocation)) 
                {
                    formFile.Seek(0, SeekOrigin.Begin);
                    formFile.CopyTo(fileStream);
                }
                _logger.LogInformation($"Writing file complete.");
                return fileStoreLocation;
            } 
            catch (Exception ex) {
                _logger.LogCritical($"Failed to write file. Error { ex.Message }");
                throw;
            }
        }
    }
}