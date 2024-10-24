using System.Text.Json.Serialization;

namespace TaskThis.Model
{
    class Part
    {
        [JsonPropertyName("text")]
        public string Text { get; set; } = string.Empty;
    }

    class Content
    {
        [JsonPropertyName("parts")]
        public Part[] Parts { get; set; } = [];
    }

    class GeminiRequest
    {
        [JsonPropertyName("contents")]
        public Content[] Contents { get; set; } = [];
    }
}