namespace CQRSRentACar.CQRSPattern.Queries.DistanceQueries
{
    public class GetDistanceQuery
    {
        public string FromCity { get; set; }
        public string ToCity { get; set; }
        public string FuelType { get; set; } = "GASOLINE";
    }
}