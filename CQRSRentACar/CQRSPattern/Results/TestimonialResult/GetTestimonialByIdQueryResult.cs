namespace CQRSRentACar.CQRSPattern.Results.TestimonialResult
{
    public class GetTestimonialByIdQueryResult
    {
        public int TestimonialId { get; set; }
        public string NameSurname { get; set; }
        public string Profession { get; set; }
        public decimal Score { get; set; }
        public string Comment { get; set; }
        public string ImageUrl { get; set; }
    }
}
