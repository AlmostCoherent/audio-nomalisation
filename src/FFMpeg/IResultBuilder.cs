namespace FFMpeg
{
    public interface IResultBuilder
    {
        void Add(string line);

        NormalizationResult Build();
    }
}