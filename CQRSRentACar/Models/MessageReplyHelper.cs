using System.Text;
using System.Text.Json;

namespace CQRSRentACar.Models
{
    public class MessageReplyHelper
    {
        private readonly string _apiKey;
        private readonly HttpClient _httpClient;

        public MessageReplyHelper(string apiKey)
        {
            _apiKey = apiKey;
            _httpClient = new HttpClient();
        }

        public async Task<string> GenerateAutoReply(string userMessage, string userName, string userEmail, string subject)
        {
            try
            {
                var prompt = $@"Sen bir araç kiralama firmasının (Rent A Car) profesyonel müşteri temsilcisisin.

Kullanıcı Bilgileri:
- İsim: {userName}
- Email: {userEmail}
- Konu: {subject}

Kullanıcının Mesajı:
{userMessage}

Görevin:
1. Kullanıcının mesajını hangi dilde yazdığını algıla (Türkçe, İngilizce, Fransızca, İtalyanca, Almanca vb.)
2. Mesajdan önemli bilgileri çıkar:
   - Tarih/saat bilgisi var mı?
   - Konum/havalimanı bilgisi var mı?
   - Kaç gün kiralama isteniyor?
   - Araç tercihi var mı?
3. AYNI DİLDE profesyonel bir otomatik cevap oluştur:
   - Firmamızı seçtiği için teşekkür et
   - Mesajını aldığımızı belirt
   - Eğer tarih/konum bilgisi verdiyse, bunları onaylayarak bahset
   - Uygun araçlar için ekibimizin en kısa sürede detaylı bilgi göndereceğini söyle
   - İletişim bilgilerimizi ekle: info@example.com | 0538 697 86 40
   - İmza: Cental Rent A Car Ekibi

ÖNEMLİ: Sadece email metnini ver, başka açıklama ekleme. Doğrudan gönderilmeye hazır bir email formatında yaz.";

                var requestBody = new
                {
                    contents = new[]
                    {
                        new
                        {
                            parts = new[]
                            {
                                new { text = prompt }
                            }
                        }
                    }
                };

                var json = JsonSerializer.Serialize(requestBody);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync(
     $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.5-flash:generateContent?key={_apiKey}",
     content
 );
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var jsonDocument = JsonDocument.Parse(responseContent);

                    var text = jsonDocument.RootElement
                        .GetProperty("candidates")[0]
                        .GetProperty("content")
                        .GetProperty("parts")[0]
                        .GetProperty("text")
                        .GetString();

                    return text ?? "Mesajınız alındı, en kısa sürede dönüş yapılacaktır.";
                }
                else
                {
                    return "Otomatik cevap oluşturulamadı. Ekibimiz en kısa sürede size dönüş yapacaktır.";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Gemini AI Error: {ex.Message}");
                return "Mesajınız başarıyla alındı. En kısa sürede size dönüş yapılacaktır.";
            }
        }
    }
}