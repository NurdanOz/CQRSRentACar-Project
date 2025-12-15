namespace CQRSRentACar.Entities
{
    public class Testimonial
    {
        public int TestimonialId { get; set; }
        public string NameSurname { get; set; }
        public string Profession { get; set; }
        public decimal Score { get; set; }
        public string Comment { get; set; }
        public string ImageUrl { get; set; }
    }
}
