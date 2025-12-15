namespace CQRSRentACar.CQRSPattern.Queries.CarQueries
{
    public class GetCarByIdQuery
    {
        public int Id { get; set; }

        public GetCarByIdQuery(int ıd)
        {
            Id = ıd;
        }
    }
}
