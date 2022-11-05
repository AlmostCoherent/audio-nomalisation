namespace Files.Abstractions
{
    public interface IFileRemover
    {
        bool RemoveByPath(string path);
    }
}