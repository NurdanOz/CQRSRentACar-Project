namespace CQRSRentACar.CQRSPattern.Queries.BookingQueries
{
    public class GetBookingByIdQuery
    {
        public int Id { get; set; }

        public GetBookingByIdQuery(int ıd)
        {
            Id = ıd;
        }
    }
}
