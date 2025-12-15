using CQRSRentACar.CQRSPattern.Queries.FuelPriceQueries;
using CQRSRentACar.CQRSPattern.Results.FuelPriceResults;
using CQRSRentACar.Models;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Globalization;

namespace CQRSRentACar.CQRSPattern.Handlers.FuelPriceHandlers
{
    public class GetFuelPricesQueryHandler
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        // Önce USA'yı deneyelim (Türkiye çalışmıyor gibi)
        private const string ApiUrl = "https://gas-price.p.rapidapi.com/stateUsaPrice?state=TURKEY";

        public GetFuelPricesQueryHandler(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("X-RapidAPI-Key", _configuration["RapidAPISettings:ApiKey"]);
            _httpClient.DefaultRequestHeaders.Add("X-RapidAPI-Host", "gas-price.p.rapidapi.com");
        }

        public async Task<GetFuelPricesQueryResult> Handle(GetFuelPricesQuery query)
        {
            var fuelPrices = new List<FuelPrice>();

            try
            {
                var response = await _httpClient.GetAsync(ApiUrl);

                // Hata durumunu logla
                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"API HATA: {response.StatusCode} - {errorContent}");
                    return new GetFuelPricesQueryResult { FuelPrices = fuelPrices };
                }

                var jsonString = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"API RESPONSE: {jsonString}"); // Debug için

                var apiResponse = JsonSerializer.Deserialize<GasPriceApiResponse>(jsonString,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (apiResponse?.Success == true && apiResponse.Result?.State != null)
                {
                    var stateData = apiResponse.Result.State.FirstOrDefault();

                    if (stateData != null)
                    {
                        var culture = CultureInfo.InvariantCulture;

                        // TL'ye çevir (1 USD = yaklaşık 34 TL, gerçek kur kullanabilirsin)
                        decimal usdToTry = 34.5M;

                        AddFuelIfValid("Benzin (Gasoline)", stateData.GasolinePrice, culture, fuelPrices, usdToTry);
                        AddFuelIfValid("Motorin (Diesel)", stateData.DieselPrice, culture, fuelPrices, usdToTry);
                        AddFuelIfValid("Orta Benzin (Midgrade)", stateData.MidgradePrice, culture, fuelPrices, usdToTry);
                        AddFuelIfValid("Premium Benzin", stateData.PremiumPrice, culture, fuelPrices, usdToTry);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"EXCEPTION: {ex.Message}");
                Console.WriteLine($"STACK TRACE: {ex.StackTrace}");
            }

            return new GetFuelPricesQueryResult { FuelPrices = fuelPrices };
        }

        private void AddFuelIfValid(string type, string? value, CultureInfo culture, List<FuelPrice> list, decimal exchangeRate)
        {
            if (decimal.TryParse(value, NumberStyles.Any, culture, out decimal priceUsd))
            {
                // Galon başına fiyat, litre başına çevir (1 galon = 3.785 litre)
                decimal pricePerLiter = priceUsd / 3.785M;

                // TL'ye çevir
                decimal priceTry = pricePerLiter * exchangeRate;

                list.Add(new FuelPrice
                {
                    FuelType = type,
                    Price = Math.Round(priceTry, 2)
                });
            }
        }
    }

    // Model sınıfları - NAMESPACE İÇİNDE OLMALI!
    public class GasPriceApiResponse
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }

        [JsonPropertyName("result")]
        public ResultWrapper Result { get; set; }
    }

    public class ResultWrapper
    {
        [JsonPropertyName("state")]
        public List<PriceDetails> State { get; set; }

        [JsonPropertyName("cities")]
        public List<object> Cities { get; set; }
    }

    public class PriceDetails
    {
        [JsonPropertyName("currency")]
        public string Currency { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("gasoline")]
        public string GasolinePrice { get; set; }

        [JsonPropertyName("midGrade")]
        public string MidgradePrice { get; set; }

        [JsonPropertyName("premium")]
        public string PremiumPrice { get; set; }

        [JsonPropertyName("diesel")]
        public string DieselPrice { get; set; }
    }
}