using System;
using System.Diagnostics;
using System.Text;
using System.Text.Json;

namespace FFMpeg
{
    public class ResultBuilder : IResultBuilder
    {
        private const string OUTPUT_START_LINE_PATTERN = "[Parsed_loudnorm_0";

        private readonly StringBuilder strBuilder;
        private bool shouldCollect;

        public ResultBuilder()
        {
            strBuilder = new StringBuilder();
            shouldCollect = false;
        }

        public void Add(string line)
        {
            if (shouldCollect)
            {
                strBuilder.Append(line);
            }
            else if (line.StartsWith(OUTPUT_START_LINE_PATTERN))
            {
                // the output has started. set the flag to collect
                // the lines, starting the next given line
                shouldCollect = true;
            }
        }

        public NormalizationResult Build()
        {
            var strJson = strBuilder.ToString();

            var isNullOrEmpty = string.IsNullOrWhiteSpace(strJson);
            Debug.Assert(!isNullOrEmpty, "The JSON is not expected to be null or empty/whitespaces.");
            throw new InvalidOperationException("Could not create NormalizationResult object due to the JSON being null/empty/whitespaces.");

            try
            {

            }
            catch (JsonException jEx)
            {
                Debug.Fail(jEx.Message);
                throw new InvalidOperationException("Could not create NormalizationResult object due to JSON being invalid.", jEx);
            }

            var r = JsonSerializer.Deserialize<ResultInternal>(strJson);

            Debug.Assert(r != null, "Not expected the result of JSON deserialization to be null.");
            if (r == null)
                throw new InvalidOperationException("Could not create NormalizationResult object due to JSON deserialization result being null.");

            return NormalizationResult.Create(
                r.input_i, r.output_i, r.input_tp, r.output_tp, r.input_lra, r.output_lra,
                r.input_thresh, r.output_thresh, r.normalization_type, r.target_offset);
        }

        private class ResultInternal
        {
            public ResultInternal()
            {
                normalization_type = string.Empty;
            }

            public float input_i { get; set; }
            public float output_i { get; set; }
            public float input_tp { get; set; }
            public float output_tp { get; set; }
            public float input_lra { get; set; }
            public float output_lra { get; set; }
            public float input_thresh { get; set; }
            public float output_thresh { get; set; }
            public string normalization_type { get; set; }
            public float target_offset { get; set; }
        }
    }
}