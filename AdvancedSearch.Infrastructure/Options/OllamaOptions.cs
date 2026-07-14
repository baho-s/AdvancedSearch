namespace AdvancedSearch.Infrastructure.Options
{
    public class OllamaOptions
    {
        public const string SectionName = "Ollama";
        public string BaseUrl { get; set; } = string.Empty;
        public string EmbeddingModel { get; set; } = string.Empty;
        public string ChatModel { get; set; } = string.Empty;
        public int RequestTimeoutSeconds { get; set; } = 300;
    }
}
