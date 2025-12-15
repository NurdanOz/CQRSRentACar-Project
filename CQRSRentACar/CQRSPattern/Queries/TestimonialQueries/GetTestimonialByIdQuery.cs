namespace CQRSRentACar.CQRSPattern.Queries.TestimonialQueries
{
    public class GetTestimonialByIdQuery
    {
        public int Id { get; set; }

        public GetTestimonialByIdQuery(int ıd)
        {
            Id = ıd;
        }
    }
}
