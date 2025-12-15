using CQRSRentACar.Context;
using CQRSRentACar.CQRSPattern.Commands.ServiceCommand;
using CQRSRentACar.CQRSPattern.Commands.SliderCommand;

namespace CQRSRentACar.CQRSPattern.Handlers.SliderHandlers
{
    public class RemoveSliderCommandHandler
    {
        private readonly CarContext _context;

        public RemoveSliderCommandHandler(CarContext context)
        {
            _context = context;
        }

        public async Task Handle(RemoveSliderCommand command)
        {
            var value = await _context.Sliders.FindAsync(command.SliderId);

            _context.Sliders.Remove(value);

            await _context.SaveChangesAsync();
        }
    }
}
