using System.Text.Json.Serialization;

namespace TaskThis.Model
{
    class Candidate
    {
        public required Content content { get; set; }
    }

    class GeminiResponse
    {
        [JsonPropertyName("candidates")]
        public Candidate[] Candidates { get; set; } = [];
    }
}