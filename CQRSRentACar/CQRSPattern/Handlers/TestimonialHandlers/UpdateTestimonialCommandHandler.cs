using CQRSRentACar.Context;
using CQRSRentACar.CQRSPattern.Commands.SliderCommand;
using CQRSRentACar.CQRSPattern.Commands.TestimonialCommand;

namespace CQRSRentACar.CQRSPattern.Handlers.TestimonialHandlers
{
    public class UpdateTestimonialCommandHandler
    {

        private readonly CarContext _contex;

        public UpdateTestimonialCommandHandler(CarContext contex)
        {
            _contex = contex;
        }

        public async Task Handle(UpdateTestimonialCommand command)
        {
            var value = await _contex.Testimonials.FindAsync(command.TestimonialId);

           value.NameSurname = command.NameSurname;
            value.Profession = command.Profession;
            value.Score = command.Score;
            value.Comment = command.Comment;
            value.ImageUrl = command.ImageUrl;

            await _contex.SaveChangesAsync();



        }
    }
}
