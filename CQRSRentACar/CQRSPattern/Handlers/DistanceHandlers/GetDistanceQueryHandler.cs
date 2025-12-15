// CQRSPattern/Handlers/DistanceHandlers/GetDistanceQueryHandler.cs
using CQRSRentACar.CQRSPattern.Queries.DistanceQueries;
using CQRSRentACar.CQRSPattern.Results.DistanceResults;
using CQRSRentACar.Models;
using System.Text.Json;

namespace CQRSRentACar.CQRSPattern.Handlers.DistanceHandlers
{
    public class GetDistanceQueryHandler
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        // Geoapify Geocoding API - Şehir adını koordinata çevir
        private const string GeocodeUrl = "https://api.geoapify.com/v1/geocode/search";

        // Geoapify Routing API - Mesafe hesapla
        private const string RoutingUrl = "https://api.geoapify.com/v1/routing";

        public GetDistanceQueryHandler(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<GetDistanceQueryResult> Handle(GetDistanceQuery query)
        {
            Console.WriteLine("=== HANDLER ÇAĞRILDI ===");
            Console.WriteLine($"FromCity: {query.FromCity}");
            Console.WriteLine($"ToCity: {query.ToCity}");

            try
            {
                // 1. Mesafe API çağrısı
                var distance = await GetDistanceFromGeoapify(query.FromCity, query.ToCity);

                if (distance == 0)
                {
                    return new GetDistanceQueryResult
                    {
                        Success = false,
                        Message = "Mesafe hesaplanamadı"
                    };
                }

                // 2. Güncel yakıt fiyatı (manuel)
                var fuelPrice = GetFuelPrice(query.FuelType);

                // 3. Hesaplamalar
                double avgConsumption = query.FuelType == "GASOLINE" ? 7.0 : 6.0;
                decimal fuelCost = (decimal)((distance / 100) * avgConsumption) * fuelPrice;
                decimal extraFee = (decimal)distance * 0.5m; // Km başına 0.50 TL
                int estimatedDuration = (int)(distance / 80 * 60); // Ortalama 80 km/h

                var result = new DistanceCalculationResult
                {
                    FromCity = query.FromCity,
                    ToCity = query.ToCity,
                    DistanceKm = distance,
                    EstimatedDurationMinutes = estimatedDuration,
                    FuelType = query.FuelType,
                    CurrentFuelPrice = fuelPrice,
                    AverageFuelConsumption = avgConsumption,
                    FuelCost = Math.Round(fuelCost, 2),
                    ExtraFee = Math.Round(extraFee, 2),
                    TotalCost = Math.Round(fuelCost + extraFee, 2)
                };

                return new GetDistanceQueryResult
                {
                    Success = true,
                    Data = result,
                    Message = "Mesafe başarıyla hesaplandı"
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Distance Calculation Error: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                return new GetDistanceQueryResult
                {
                    Success = false,
                    Message = $"Hata: {ex.Message}"
                };
            }
        }

        private async Task<double> GetDistanceFromGeoapify(string fromCity, string toCity)
        {
            try
            {
                var apiKey = _configuration["GeoapifySettings:ApiKey"];
                Console.WriteLine($"API Key: {apiKey ?? "NULL!"}");

                // 1. Şehir adlarını koordinata çevir
                var fromCoords = await GetCoordinates(fromCity, apiKey);
                var toCoords = await GetCoordinates(toCity, apiKey);

                if (fromCoords == null || toCoords == null)
                {
                    Console.WriteLine("Koordinatlar alınamadı");
                    return 0;
                }

                Console.WriteLine($"From Coords: {fromCoords.Lat}, {fromCoords.Lon}");
                Console.WriteLine($"To Coords: {toCoords.Lat}, {toCoords.Lon}");

                // 2. Routing API ile mesafe hesapla
                var url = $"{RoutingUrl}?waypoints={fromCoords.Lat},{fromCoords.Lon}|{toCoords.Lat},{toCoords.Lon}&mode=drive&apiKey={apiKey}";
                Console.WriteLine($"Routing URL: {url}");

                var response = await _httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Geoapify Routing Error: {response.StatusCode} - {error}");
                    return 0;
                }

                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Geoapify Response: {content}");

                var routeResponse = JsonSerializer.Deserialize<GeoapifyRouteResponse>(content,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                // Mesafe metre cinsinden geliyor, km'ye çevir
                var distanceMeters = routeResponse?.Features?.FirstOrDefault()?.Properties?.Distance ?? 0;
                Console.WriteLine($"Distance Meters: {distanceMeters}");

                return distanceMeters / 1000.0; // km'ye çevir
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetDistanceFromGeoapify Error: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                return 0;
            }
        }

        private async Task<Coordinates> GetCoordinates(string cityName, string apiKey)
        {
            try
            {
                Console.WriteLine($"Getting coordinates for: {cityName}");

                // Türkiye için şehir araması
                var url = $"{GeocodeUrl}?text={cityName},Turkey&format=json&apiKey={apiKey}";
                Console.WriteLine($"Geocode URL: {url}");

                var response = await _httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Geocode Error: {response.StatusCode}");
                    return null;
                }

                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Geocode Response: {content}");

                var geocodeResponse = JsonSerializer.Deserialize<GeoapifyGeocodeResponse>(content,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                var firstResult = geocodeResponse?.Results?.FirstOrDefault();

                if (firstResult == null)
                {
                    Console.WriteLine($"Koordinat bulunamadı: {cityName}");
                    return null;
                }

                Console.WriteLine($"Coordinates found for {cityName}: {firstResult.Lat}, {firstResult.Lon}");

                return new Coordinates
                {
                    Lat = firstResult.Lat,
                    Lon = firstResult.Lon
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetCoordinates Error: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                return null;
            }
        }

        private decimal GetFuelPrice(string fuelType)
        {
            return fuelType switch
            {
                "GASOLINE" => 27.69m,
                "DIESEL" => 33.60m,
                "MIDGRADE" => 32.31m,
                "PREMIUM" => 35.56m,
                _ => 27.69m
            };
        }
    }

    // Geoapify Response Models
    public class GeoapifyGeocodeResponse
    {
        public List<GeocodeResult> Results { get; set; }
    }

    public class GeocodeResult
    {
        public double Lat { get; set; }
        public double Lon { get; set; }
    }

    public class Coordinates
    {
        public double Lat { get; set; }
        public double Lon { get; set; }
    }

    public class GeoapifyRouteResponse
    {
        public List<RouteFeature> Features { get; set; }
    }

    public class RouteFeature
    {
        public RouteProperties Properties { get; set; }
    }

    public class RouteProperties
    {
        public double Distance { get; set; } // metre cinsinden
        public double Time { get; set; } // saniye cinsinden
    }
}