namespace FFMpeg.Abstractions
{
    public interface ILUFSProvider
    {
        void SetLufsOfInput(string inputFile);
    }
}