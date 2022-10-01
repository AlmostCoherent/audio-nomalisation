namespace FFMpeg.Abstractions
{
    public interface ILUFSProvider
    {
        /// <summary>
        /// A method 
        /// </summary>
        /// <param name="inputFile">Path to the file to normalise.</param>
        /// <returns>Path to the produced audio file.</returns>
        string AdjustLufsOfInput(string inputFile);
    }
}