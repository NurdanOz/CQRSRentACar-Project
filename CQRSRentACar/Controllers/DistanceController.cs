using CQRSRentACar.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace CQRSRentACar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DistanceController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public DistanceController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClient = httpClientFactory.CreateClient();
            _configuration = configuration;
        }

        [HttpGet("calculate")]
        public async Task<IActionResult> CalculateDistance(
            [FromQuery] string fromCity,
            [FromQuery] string toCity,
            [FromQuery] string fuelType = "GASOLINE")
        {
            if (string.IsNullOrEmpty(fromCity) || string.IsNullOrEmpty(toCity))
            {
                return BadRequest(new { success = false, message = "Şehir bilgileri gerekli" });
            }

            Console.WriteLine("=== DISTANCE API ÇAĞRILDI ===");
            Console.WriteLine($"FromCity: {fromCity}");
            Console.WriteLine($"ToCity: {toCity}");
            Console.WriteLine($"FuelType: {fuelType}");

            try
            {
                // RapidAPI ile GERÇEK mesafe hesapla
                var distance = await CalculateRealDistance(fromCity, toCity);
                Console.WriteLine($"✅ Mesafe: {distance:F1} km");

                var fuelPrice = GetFuelPrice(fuelType);
                double avgConsumption = fuelType == "GASOLINE" ? 7.0 : 6.0;
                decimal fuelCost = (decimal)((distance / 100) * avgConsumption) * fuelPrice;
                decimal extraFee = (decimal)distance * 0.5m;
                int estimatedDuration = (int)(distance / 80 * 60);

                var result = new DistanceCalculationResult
                {
                    FromCity = NormalizeCityName(fromCity),
                    ToCity = NormalizeCityName(toCity),
                    DistanceKm = distance,
                    EstimatedDurationMinutes = estimatedDuration,
                    FuelType = fuelType,
                    CurrentFuelPrice = fuelPrice,
                    AverageFuelConsumption = avgConsumption,
                    FuelCost = fuelCost,
                    ExtraFee = extraFee,
                    TotalCost = fuelCost + extraFee
                };

                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ HATA: {ex.Message}");
                return StatusCode(500, new { success = false, message = $"Hata: {ex.Message}" });
            }
        }

        private async Task<double> CalculateRealDistance(string fromCity, string toCity)
        {
            try
            {
                // Şehir adlarını temizle
                fromCity = CleanCityName(fromCity);
                toCity = CleanCityName(toCity);

                var fromCoords = GetCityCoordinates(fromCity);
                var toCoords = GetCityCoordinates(toCity);

                Console.WriteLine($"📍 {fromCity} → ({fromCoords.lat}, {fromCoords.lon})");
                Console.WriteLine($"📍 {toCity} → ({toCoords.lat}, {toCoords.lon})");

                var rapidApiKey = _configuration["RapidAPISettings:ApiKey"];

                if (string.IsNullOrEmpty(rapidApiKey))
                {
                    Console.WriteLine("⚠️ RapidAPI Key yok, fallback hesaplama...");
                    return CalculateFallbackDistance(fromCoords, toCoords);
                }

                // RapidAPI Distance Calculator
                var url = $"https://distance-calculator8.p.rapidapi.com/calc?" +
                         $"startLatitude={fromCoords.lat}" +
                         $"&startLongitude={fromCoords.lon}" +
                         $"&endLatitude={toCoords.lat}" +
                         $"&endLongitude={toCoords.lon}";

                Console.WriteLine($"🔍 RapidAPI Distance Calculator çağrılıyor...");

                _httpClient.DefaultRequestHeaders.Clear();
                _httpClient.DefaultRequestHeaders.Add("x-rapidapi-key", rapidApiKey);
                _httpClient.DefaultRequestHeaders.Add("x-rapidapi-host", "distance-calculator8.p.rapidapi.com");

                var response = await _httpClient.GetAsync(url);
                var content = await response.Content.ReadAsStringAsync();

                Console.WriteLine($"RapidAPI Status: {response.StatusCode}");

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"❌ API hatası: {content}");
                    return CalculateFallbackDistance(fromCoords, toCoords);
                }

                var json = JsonSerializer.Deserialize<JsonDocument>(content);

                Console.WriteLine($"📦 Response Content: {content}");

                // Response yapısını kontrol et
                double distanceKm = 0;

                if (json.RootElement.TryGetProperty("body", out var bodyElement))
                {
                    if (bodyElement.TryGetProperty("distance", out var distanceElement))
                    {
                        if (distanceElement.TryGetProperty("kilometers", out var kmElement))
                        {
                            distanceKm = kmElement.GetDouble();
                        }
                    }
                }

                // Eğer distance 0 ise, fallback yap
                if (distanceKm <= 0)
                {
                    Console.WriteLine("⚠️ API'den mesafe 0 geldi, fallback yapılıyor...");
                    return CalculateFallbackDistance(fromCoords, toCoords);
                }

                Console.WriteLine($"✅ RapidAPI'den GERÇEK mesafe: {distanceKm:F1} km");

                return distanceKm;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ RapidAPI hatası: {ex.Message}");
                Console.WriteLine("⚠️ Fallback hesaplama yapılıyor...");

                var fromCoords = GetCityCoordinates(CleanCityName(fromCity));
                var toCoords = GetCityCoordinates(CleanCityName(toCity));

                return CalculateFallbackDistance(fromCoords, toCoords);
            }
        }

        private double CalculateFallbackDistance((double lat, double lon) fromCoords, (double lat, double lon) toCoords)
        {
            // Haversine formülü
            double R = 6371; // Dünya'nın yarıçapı (km)

            double lat1 = fromCoords.lat * Math.PI / 180;
            double lat2 = toCoords.lat * Math.PI / 180;
            double dLat = (toCoords.lat - fromCoords.lat) * Math.PI / 180;
            double dLon = (toCoords.lon - fromCoords.lon) * Math.PI / 180;

            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                      Math.Cos(lat1) * Math.Cos(lat2) *
                      Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            double distance = R * c;

            // Yol mesafesi kuş uçuşundan %30 daha fazla
            double roadDistance = distance * 1.3;

            Console.WriteLine($"📐 Fallback hesaplama: {roadDistance:F1} km");

            return roadDistance;
        }

        private string CleanCityName(string cityName)
        {
            cityName = cityName.Replace("Havalimanı", "")
                               .Replace("Havalimani", "")
                               .Replace("(IST)", "").Replace("(ADA)", "")
                               .Replace("(ESB)", "").Replace("(AYT)", "")
                               .Replace("(SAW)", "").Replace("(ADB)", "")
                               .Trim();

            // İlk kelimeyi al (şehir adı)
            var parts = cityName.Split(' ');
            return parts[0];
        }

        private (double lat, double lon) GetCityCoordinates(string cityName)
        {
            cityName = cityName.Replace("ı", "i").Replace("İ", "I")
                               .Replace("ş", "s").Replace("Ş", "S")
                               .Replace("ğ", "g").Replace("Ğ", "G")
                               .Replace("ü", "u").Replace("Ü", "U")
                               .Replace("ö", "o").Replace("Ö", "O")
                               .Replace("ç", "c").Replace("Ç", "C");

            var coordinates = new Dictionary<string, (double lat, double lon)>()
            {
                { "Istanbul", (41.0082, 28.9784) },
                { "Ankara", (39.9334, 32.8597) },
                { "Izmir", (38.4237, 27.1428) },
                { "Antalya", (36.8969, 30.7133) },
                { "Adana", (37.0000, 35.3213) },
                { "Bursa", (40.1826, 29.0665) },
                { "Gaziantep", (37.0662, 37.3833) },
                { "Konya", (37.8667, 32.4833) },
                { "Kayseri", (38.7205, 35.4826) },
                { "Mersin", (36.8000, 34.6333) },
                { "Eskisehir", (39.7767, 30.5206) },
                { "Diyarbakir", (37.9144, 40.2306) },
                { "Samsun", (41.2867, 36.33) },
                { "Denizli", (37.7765, 29.0864) },
                { "Trabzon", (41.0015, 39.7178) }
            };

            if (coordinates.ContainsKey(cityName))
            {
                return coordinates[cityName];
            }

            Console.WriteLine($"⚠️ Şehir bulunamadı: {cityName}, varsayılan Istanbul");
            return (41.0082, 28.9784);
        }

        private decimal GetFuelPrice(string fuelType)
        {
            return fuelType == "GASOLINE" ? 55.00m : 57.00m;
        }

        private string NormalizeCityName(string cityName)
        {
            // Türkçe şehir adlarını düzgün göster
            var cityMap = new Dictionary<string, string>()
    {
        { "Istanbul", "İstanbul" },
        { "Izmir", "İzmir" },
        { "Ankara", "Ankara" },
        { "Antalya", "Antalya" },
        { "Adana", "Adana" },
        { "Bursa", "Bursa" },
        { "Gaziantep", "Gaziantep" },
        { "Konya", "Konya" }
    };

            // Şehir adını temizle
            var cleanName = CleanCityName(cityName);

            // Eğer mapte varsa Türkçe halini döndür
            if (cityMap.ContainsKey(cleanName))
            {
                return cityMap[cleanName];
            }

            return cleanName;
        }
    }
}