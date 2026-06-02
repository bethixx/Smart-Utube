namespace Smart_Utube.DTOs.Quote
{
    using System.Text.Json.Serialization;

    public class QuoteDto
    {
        [JsonPropertyName("quote")]
        public string Quote { get; set; } = "";

        [JsonPropertyName("author")]
        public string Author { get; set; } = "";
    }
}