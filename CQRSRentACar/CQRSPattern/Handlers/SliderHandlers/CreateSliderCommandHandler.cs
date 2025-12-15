using CQRSRentACar.Context;
using CQRSRentACar.CQRSPattern.Commands.ServiceCommand;
using CQRSRentACar.CQRSPattern.Commands.SliderCommand;

namespace CQRSRentACar.CQRSPattern.Handlers.SliderHandlers
{
    public class CreateSliderCommandHandler
    {
        private readonly CarContext _context;

        public CreateSliderCommandHandler(CarContext context)
        {
            _context = context;
        }

        public async Task Handle(CreateSliderCommand command)
        {
            _context.Sliders.Add(new Entities.Slider
            {
                
                Title = command.Title,
                Description = command.Description,
                ImageUrl = command.ImageUrl



            });

            await _context.SaveChangesAsync();
        }
    }
}
