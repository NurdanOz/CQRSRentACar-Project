namespace CQRSRentACar.CQRSPattern.Results.LocationResults
{
    public class GetLocationByIdQueryResult
    {
        public int LocationId { get; set; }
        public string LocationName { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string CountryCode { get; set; }
        public string CityCode { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
    }
}
