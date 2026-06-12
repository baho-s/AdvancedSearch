using AdvancedSearch.Domain.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AdvancedSearch.Infrastructure.Services
{
    public class OllamaEmbeddingService : IEmbeddingService
    {
        private readonly HttpClient _httpClient;
        // nomic-embed-text: 768 boyutlu vektör üretir, Türkçe destekler
        private const string Model = "nomic-embed-text";

        public OllamaEmbeddingService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://localhost:11434");
        }

        public async Task<float[]> GenerateEmbeddingAsync(string text)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/embeddings", new
            {
                model = Model,
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
