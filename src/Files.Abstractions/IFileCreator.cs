namespace Files.Abstractions
{
    public interface IFileCreator
    {
        /// <summary>
        /// Writes a file to a folder from a <see cref="Stream"/>. 
        /// </summary>
        /// <param name="formFile"></param>
        /// <returns></returns>
        Task<string> CreateFromStream(Stream formFile);
    }
}