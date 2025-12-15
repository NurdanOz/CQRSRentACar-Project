namespace CQRSRentACar.CQRSPattern.Commands.TestimonialCommand
{
    public class RemoveTestimonialCommand
    {
        public int TestimonialId { get; set; }

        public RemoveTestimonialCommand(int testimonialId)
        {
            TestimonialId = testimonialId;
        }
    }
}
