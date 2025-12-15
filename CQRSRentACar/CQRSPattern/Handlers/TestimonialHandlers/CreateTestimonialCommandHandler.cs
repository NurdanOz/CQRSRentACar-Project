using CQRSRentACar.Context;
using CQRSRentACar.CQRSPattern.Commands.SliderCommand;
using CQRSRentACar.CQRSPattern.Commands.TestimonialCommand;

namespace CQRSRentACar.CQRSPattern.Handlers.TestimonialHandlers
{
    public class CreateTestimonialCommandHandler
    {
        private readonly CarContext _context;

        public CreateTestimonialCommandHandler(CarContext context)
        {
            _context = context;
        }

        public async Task Handle(CreateTestimonialCommand command)
        {
            _context.Testimonials.Add(new Entities.Testimonial
            {

                NameSurname = command.NameSurname,
                Profession = command.Profession,
                Score = command.Score,
                Comment = command.Comment,
                ImageUrl = command.ImageUrl




            });

            await _context.SaveChangesAsync();
        }
    }
}
