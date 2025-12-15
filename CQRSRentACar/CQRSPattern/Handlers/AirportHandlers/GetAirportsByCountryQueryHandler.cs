using CQRSRentACar.CQRSPattern.Queries.AirportQueries;
using CQRSRentACar.CQRSPattern.Results.AirportResults;
using CQRSRentACar.Models;
using System.Text.Json;

namespace CQRSRentACar.CQRSPattern.Handlers.AirportHandlers
{
    public class GetAirportsByCountryQueryHandler
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public GetAirportsByCountryQueryHandler(IConfiguration configuration, HttpClient httpClient)
        {
            _configuration = configuration;
            _httpClient = httpClient;
        }

        public async Task<GetAirportsByCountryQueryResult> Handle(GetAirportsByCountryQuery query)
        {
            var result = new GetAirportsByCountryQueryResult();
            
            // ✅ MOCK DATA - API hakkı bitince direkt bunu kullan
            result.Airports = GetMockTurkeyAirports();
            result.Success = true;
            result.Message = $"{result.Airports.Count} havalimanı bulundu.";
            
            return result;
        }

        // ✅ TÜRKİYE HAVALİMANLARI - MANUEL LİSTE
        private List<Airport> GetMockTurkeyAirports()
        {
            return new List<Airport>
            {
                new Airport { Name = "İstanbul Havalimanı", Iata = "IST", Icao = "LTFM" },
                new Airport { Name = "Sabiha Gökçen Havalimanı", Iata = "SAW", Icao = "LTFJ" },
                new Airport { Name = "Esenboğa Havalimanı", Iata = "ESB", Icao = "LTAC" },
                new Airport { Name = "Antalya Havalimanı", Iata = "AYT", Icao = "LTAI" },
                new Airport { Name = "İzmir Adnan Menderes Havalimanı", Iata = "ADB", Icao = "LTBJ" },
                new Airport { Name = "Dalaman Havalimanı", Iata = "DLM", Icao = "LTBS" },
                new Airport { Name = "Milas-Bodrum Havalimanı", Iata = "BJV", Icao = "LTFE" },
                new Airport { Name = "Trabzon Havalimanı", Iata = "TZX", Icao = "LTCG" },
                new Airport { Name = "Gaziantep Havalimanı", Iata = "GZT", Icao = "LTAJ" },
                new Airport { Name = "Adana Havalimanı", Iata = "ADA", Icao = "LTAF" },
                new Airport { Name = "Kayseri Havalimanı", Iata = "ASR", Icao = "LTAU" },
                new Airport { Name = "Diyarbakır Havalimanı", Iata = "DIY", Icao = "LTCC" },
                new Airport { Name = "Van Ferit Melen Havalimanı", Iata = "VAN", Icao = "LTCI" }
            };
        }
    }
}