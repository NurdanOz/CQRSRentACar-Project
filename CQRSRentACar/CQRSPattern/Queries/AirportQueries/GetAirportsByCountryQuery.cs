namespace CQRSRentACar.CQRSPattern.Queries.AirportQueries
{
    public class GetAirportsByCountryQuery
    {
        public string CountryCode { get; set; }

        public GetAirportsByCountryQuery(string countryCode)
        {
            CountryCode = countryCode;
        }
    }
}