namespace CQRSRentACar.CQRSPattern.Queries.FeatureQueries
{
    public class GetFeatureByIdQuery
    {
        public int Id { get; set; }

        public GetFeatureByIdQuery(int ıd)
        {
            Id = ıd;
        }
    }
}
