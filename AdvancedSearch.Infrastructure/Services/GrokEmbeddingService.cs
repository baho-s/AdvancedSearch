using AdvancedSearch.Domain.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedSearch.Infrastructure.Services
{
    public class GrokEmbeddingService : IEmbeddingService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        // Grok'un ücretsiz embedding modeli
        private const string Model = "multilingual-e5-large";

        public GrokEmbeddingService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["GrokApiKey"];
        }
        public async Task<float[]> GetEmbeddingAsync(string text)
        {
            var request = new HttpRequestMessage(HttpMethod.Post,
             "https://api.x.ai/v1/embeddings");

            request.Headers.Authorization=
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _apiKey);

            request.Content = JsonContent.Create(new
            {
                model=Model,
                input=text
            });

            var response= await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var result=await response.Content.ReadFromJsonAsync<GrokEmbeddingResponse>();

            return result!.Data[0].Embedding;
        }
    }
    internal record GrokEmbeddingResponse(List<GrokEmbeddingData> Data);//internal:Aynı assembly içinde erişilebilir, diğer assembly'lerden erişilemez
    internal record GrokEmbeddingData(float[] Embedding);//record:immutable veri yapıları oluşturmak için kullanılır, otomatik olarak özellikler, yapıcılar ve diğer yardımcı üyeler oluşturur
}
