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

        public string CreateFromStream(Stream formFile, string fileName, string? path = default)
        {
            var fileStoreLocation = Path.Combine((path ?? AppContext.BaseDirectory), fileName);
            try {
                using (var fileStream = File.Create(fileStoreLocation))
                {
                    formFile.Seek(0, SeekOrigin.Begin);
                    formFile.CopyTo(fileStream);
                }
                return fileStoreLocation;
            } 
            catch (Exception ex) {
                throw;
            }
        }
    }
}