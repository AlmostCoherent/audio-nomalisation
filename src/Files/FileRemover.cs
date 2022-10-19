namespace Files
{
	public class FileRemover
    {
        public bool RemoveByPath(string path)
        {
            if (File.Exists(path))
            {
                try
                {
                    File.Delete(path);
                    return true;
                }
                catch(Exception ex)
                {
                    throw;
                }
            }
            return false;
        }
    }
}
