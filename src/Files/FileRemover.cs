using Files.Abstractions;
using Microsoft.Extensions.Logging;

namespace Files
{
    public class FileRemover : IFileRemover
    {
        private readonly ILogger<FileCreator> _logger;

        public FileRemover(ILogger<FileCreator> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public bool RemoveByPath(string path)
        {
            _logger.LogInformation($"File stored location: {path}");
            _logger.LogInformation($"Deleting file.");
            if (File.Exists(path))
            {
                try
                {
                    File.Delete(path);
                    _logger.LogInformation($"Delete file complete.");
                    return true;
                }
                catch (Exception ex)
                {
                    _logger.LogCritical($"Failed to delete file. Error {ex.Message}");
                    throw;
                }
            }
            return false;
        }
    }
}
