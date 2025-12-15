namespace CQRSRentACar.CQRSPattern.Queries.MessageQueries
{
    public class GetMessageByIdQuery
    {
        public int Id { get; set; }

        public GetMessageByIdQuery(int ıd)
        {
            Id = ıd;
        }
    }
}
