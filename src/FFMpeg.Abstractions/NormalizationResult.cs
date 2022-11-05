namespace FFMpeg.Abstractions
{
    public class NormalizationResult
    {
        public static readonly NormalizationResult Empty;

        static NormalizationResult()
        {
            Empty = new NormalizationResult(null, null, null, null, null, null, null, null, null, null);
        }

        private NormalizationResult(
            float? inputI, float? outputI,
            float? inputTp, float? outputTp,
            float? inputLra, float? outputLra,
            float? inputThresh, float? outputThresh,
            string? normalizationType, float? targetOffset)
        {
            InputI = inputI;
            OutputI = outputI;
            InputTp = inputTp;
            OutputTp = outputTp;
            InputLra = inputLra;
            OutputLra = outputLra;
            InputThresh = inputThresh;
            OutputThresh = outputThresh;
            NormalizationType = normalizationType;
            TargetOffset = targetOffset;
        }

        public static NormalizationResult Create(
            float inputI, float outputI,
            float inputTp, float outputTp,
            float inputLra, float outputLra,
            float inputThresh, float outputThresh,
            string normalizationType, float targetOffset)
        {
            return new NormalizationResult(inputI, outputI, inputTp, outputTp, inputLra, outputLra, inputThresh, outputThresh, normalizationType, targetOffset);
        }

        public float? InputI { get; }

        public float? OutputI { get; }

        public float? InputTp { get; }

        public float? OutputTp { get; }

        public float? InputLra { get; }

        public float? OutputLra { get; }

        public float? InputThresh { get; }

        public float? OutputThresh { get; }

        public string? NormalizationType { get; }

        public float? TargetOffset { get; }
    }
}