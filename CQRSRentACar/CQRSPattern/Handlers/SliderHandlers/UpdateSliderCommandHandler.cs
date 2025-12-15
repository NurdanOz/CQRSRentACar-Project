using CQRSRentACar.Context;
using CQRSRentACar.CQRSPattern.Commands.ServiceCommand;
using CQRSRentACar.CQRSPattern.Commands.SliderCommand;

namespace CQRSRentACar.CQRSPattern.Handlers.SliderHandlers
{
    public class UpdateSliderCommandHandler
    {
        private readonly CarContext _contex;

        public UpdateSliderCommandHandler(CarContext contex)
        {
            _contex = contex;
        }

        public async Task Handle(UpdateSliderCommand command)
        {
            var value = await _contex.Sliders.FindAsync(command.SliderId);

            value.Title = command.Title;
            value.Description = command.Description;
            value.ImageUrl = command.ImageUrl;

            await _contex.SaveChangesAsync();



        }
    }
}
