using System.Text;
using System.Text.Json;

namespace CQRSRentACar.Models
{
    public class GeminiHelper
    {
        private readonly string _apiKey;

        public GeminiHelper(string apiKey)
        {
            _apiKey = apiKey;
        }

        public async Task<string> GetCarRecommendation(string userPrompt, List<CarInfo> availableCars)
        {
            var httpClient = new HttpClient();

            // Araç listesini string'e çevir
            var carsText = new StringBuilder();
            foreach (var car in availableCars)
            {
                carsText.AppendLine($"- {car.Brand} {car.Model}: {car.Seat} koltuk, {car.Transmission}, {car.Fuel}, Günlük: {car.Price}₺");
            }

            // Gemini'ye gönderilecek prompt
            var systemPrompt = $@"Sen bir araç kiralama asistanısın. Kullanıcının ihtiyacına göre en uygun araçları öner.

Mevcut Araçlar:
{carsText}

Kullanıcı İsteği: {userPrompt}

Lütfen:
1. Kullanıcının ihtiyacına en uygun 3 aracı seç
2. Her araç için KISA (2-3 cümle) açıklama yap
3. Sadece mevcut araçlardan seç
4. Cevabı şu formatta ver:

ÖNERILEN ARAÇLAR:

1. [Marka Model]
[Kısa açıklama]

2. [Marka Model]
[Kısa açıklama]

3. [Marka Model]
[Kısa açıklama]";

            var requestBody = new
            {
                contents = new[]
                {
                    new
                    {
                        parts = new[]
                        {
                            new { text = systemPrompt }
                        }
                    }
                }
            };

            var json = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                var response = await httpClient.PostAsync(
      $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.5-flash:generateContent?key={_apiKey}",
      content
                  );

                var responseText = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = JsonDocument.Parse(responseText);

                    var text = jsonResponse.RootElement
                        .GetProperty("candidates")[0]
                        .GetProperty("content")
                        .GetProperty("parts")[0]
                        .GetProperty("text")
                        .GetString();

                    return text ?? "Öneri oluşturulamadı.";
                }

                // Hata mesajını göster
                return $"API Hatası: {response.StatusCode}\n{responseText}";
            }
            catch (Exception ex)
            {
                return $"Hata Detayı: {ex.Message}\n\nStack: {ex.StackTrace}";
            }
        }
    }

    public class CarInfo
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Seat { get; set; }
        public string Transmission { get; set; }
        public string Fuel { get; set; }
        public decimal Price { get; set; }
    }
}