using AdvancedSearch.Domain.Interfaces.Services;
using ShopSage.Domain.Entities;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;

namespace AdvancedSearch.Infrastructure.Services
{
    public class OllamaRagService : IRagService
    {
        private readonly HttpClient _httpClient;
        private const string ModelName = "qwen2.5";

        public OllamaRagService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://localhost:11434/");
        }

        public async Task<string> GenerateAnswerAsync(string userQuestion, List<Product> products, CancellationToken cancellationToken)
        {
            // StringBuilder hem performansı çözer hem de ilk değer sorununu ortadan kaldırır
            var contextBuilder = new StringBuilder();

            for (int i = 0; i < products.Count; i++)
            {
                contextBuilder.AppendLine($"({i + 1}. Ürün: {products[i].Name} | Fiyat: {products[i].Price} | Açıklama: {products[i].Description} )");
            }

            // En son tek bir string haline getiriyoruz
            string context = contextBuilder.ToString();

            string systemPrompt = "Sen bir e-ticaret asistanısın. Kullanıcının sorusuna, sana sağlanan ürün bilgilerini (Context) kullanarak samimi, profesyonel ve yardımcı bir dille cevap ver. Eğer sağlanan ürünler kullanıcının sorusuyla tamamen alakasız ise veya context boşsa, elindeki ürünleri zorlamadan kibarca yardımcı olamayacağını belirt. Ürün bilgilerini doğru ve eksiksiz kullan.";

            string combinedUserContent = $"Kullanıcı Sorusu: {userQuestion}\n\n[Mevcut Ürün Bilgileri (Context)]:\n{context}";

            // Ollama API'sinin beklediği JSON gövdesi
            var requestBody = new OllamaChatRequest(
                Model: ModelName,
                Messages: new List<OllamaChatMessage>
                {
                    new OllamaChatMessage("system", systemPrompt),
                    new OllamaChatMessage("user", combinedUserContent)
                },
                Stream: false // Yanıtın akış (stream) şeklinde değil, tek parça gelmesi için
            );

            try
            {
                // 3. ADIM: Ollama'ya İstek Atma ve Yanıtı Parse Etme
                var response = await _httpClient.PostAsJsonAsync("api/chat", requestBody);

                // HTTP hatası varsa exception fırlatır (Örn: 500 veya 404)
                response.EnsureSuccessStatusCode();

                // Gelen JSON'ı yazdığımız record modeline dönüştürüyoruz
                var result = await response.Content.ReadFromJsonAsync<OllamaChatResponse>();

                // LLM'den dönen nihai metni geri gönderiyoruz
                return result?.Message?.Content ?? "Asistan şu an cevap üretemiyor.";
            }

            catch (HttpRequestException ex)
            {
                // Hata durumunda loglama mekanizmanı buraya entegre edebilirsin
                return $"Ollama servisine bağlanırken bir hata oluştu: {ex.Message}";
            }
            catch (Exception ex)
            {
                return $"Sistem genel hatası: {ex.Message}";
            }
        }
    }
    public record OllamaChatRequest(
        [property: JsonPropertyName("model")] string Model,
        [property: JsonPropertyName("messages")] List<OllamaChatMessage> Messages,
        [property: JsonPropertyName("stream")] bool Stream = false
    );

    public record OllamaChatMessage(
        [property: JsonPropertyName("role")] string Role,
        [property: JsonPropertyName("content")] string Content
    );

    public record OllamaChatResponse(
        [property: JsonPropertyName("model")] string Model,
        [property: JsonPropertyName("message")] OllamaChatMessage Message,
        [property: JsonPropertyName("done")] bool Done
    );
}
