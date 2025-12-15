using CQRSRentACar.Context;
using CQRSRentACar.CQRSPattern.Commands.SliderCommand;
using CQRSRentACar.CQRSPattern.Commands.TestimonialCommand;

namespace CQRSRentACar.CQRSPattern.Handlers.TestimonialHandlers
{
    public class RemoveTestimonialCommandHandler
    {
        private readonly CarContext _context;

        public RemoveTestimonialCommandHandler(CarContext context)
        {
            _context = context;
        }

        public async Task Handle(RemoveTestimonialCommand command)
        {
            var value = await _context.Testimonials.FindAsync(command.TestimonialId);

            _context.Testimonials.Remove(value);

            await _context.SaveChangesAsync();
        }
    }
}
