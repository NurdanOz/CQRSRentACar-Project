using CQRSRentACar.Models;

namespace CQRSRentACar.CQRSPattern.Results.AirportResults
{
    public class GetAirportsByCountryQueryResult
    {
        public List<Airport> Airports { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }

        public GetAirportsByCountryQueryResult()
        {
            Airports = new List<Airport>();
            Success = true;
            Message = string.Empty;
        }
    }
}