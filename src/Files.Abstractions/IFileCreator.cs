using System.IO;

namespace Files.Abstractions
{
    public interface IFileCreator
    {
        /// <summary>
        /// Writes a file to a folder from a <see cref="Stream"/>. 
        /// </summary>
        /// <param name="formFile"></param>
        /// <returns></returns>
        string CreateFromStream(Stream formFile, string fileName, string? path = null);
    }
}