namespace CQRSRentACar.CQRSPattern.Queries.ServiceQueries
{
    public class GetServiceByIdQuery
    {
        public int Id { get; set; }

        public GetServiceByIdQuery(int ıd)
        {
            Id = ıd;
        }
    }
}
