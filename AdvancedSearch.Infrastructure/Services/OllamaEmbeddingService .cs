using AdvancedSearch.Application.Interfaces.AI;
using AdvancedSearch.Infrastructure.Options;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace AdvancedSearch.Infrastructure.Services
{
    public class OllamaEmbeddingService : IEmbeddingService
    {
        private readonly HttpClient _httpClient;
        private readonly string _model;
        public OllamaEmbeddingService(HttpClient httpClient,IOptions<OllamaOptions> options)
        {
            _httpClient = httpClient;
            _model=options.Value.EmbeddingModel;
        }

        public async Task<float[]> GenerateEmbeddingAsync(string text)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/embeddings", new
            {
                model = _model,
                prompt = text  // Ollama "prompt" kullanır.
            });

            response.EnsureSuccessStatusCode();

            var result = await response.Content
                .ReadFromJsonAsync<OllamaEmbeddingResponse>();

            return result!.Embedding;
        }
    }

    // Ollama doğrudan array döner.
    internal record OllamaEmbeddingResponse(
        [property: JsonPropertyName("embedding")] float[] Embedding
    );
}
