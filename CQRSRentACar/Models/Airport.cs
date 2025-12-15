namespace CQRSRentACar.Models
{
    public class Airport
    {
        public string Name { get; set; }
        public string Iata { get; set; }
        public string Icao { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public int Elevation { get; set; }
        public int UtcOffset { get; set; }
        public string Dst { get; set; }
        public string Timezone { get; set; }
        public AirportCountry Country { get; set; }
    }

    public class AirportCountry
    {
        public string Name { get; set; }
        public string Iso2 { get; set; }
    }

    public class AirportApiResponse
    {
        public List<Airport> Data { get; set; }
        public MetaData Meta { get; set; } // ✅ EKLENDI
    }

    
    public class MetaData
    {
        public PaginationData Pagination { get; set; }
    }

    public class PaginationData
    {
        public int Total { get; set; }
        public int PerPage { get; set; }
        public int CurrentPage { get; set; }
        public int LastPage { get; set; }
        public int From { get; set; }
        public int To { get; set; }
    }
}