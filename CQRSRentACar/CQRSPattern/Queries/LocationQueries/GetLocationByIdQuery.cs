namespace CQRSRentACar.CQRSPattern.Queries.LocationQueries
{
    public class GetLocationByIdQuery
    {
        public int Id { get; set; }

        public GetLocationByIdQuery(int ıd)
        {
            Id = ıd;
        }
    }
}
