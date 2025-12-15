namespace CQRSRentACar.CQRSPattern.Commands.TestimonialCommand
{
    public class CreateTestimonialCommand
    {
        public string NameSurname { get; set; }
        public string Profession { get; set; }
        public decimal Score { get; set; }
        public string Comment { get; set; }
        public string ImageUrl { get; set; }
    }
}
