using CQRSRentACar.Context;
using CQRSRentACar.CQRSPattern.Commands.EmployeeCommand;
using CQRSRentACar.CQRSPattern.Commands.FeatureCommand;

namespace CQRSRentACar.CQRSPattern.Handlers.FeatureHandlers
{
    public class UpdateFeatureCommandHandler
    {
        private readonly CarContext _contex;

        public UpdateFeatureCommandHandler(CarContext contex)
        {
            _contex = contex;
        }

        public async Task Handle(UpdateFeatureCommand command)
        {
            var value = await _contex.Features.FindAsync(command.FeatureId);

            value.Title = command.Title;
            value.Title2 = command.Title2;
            value.Description = command.Description;
            value.Description2 = command.Description2;
            value.ImageURL = command.ImageURL;
            value.Icon = command.Icon;

            await _contex.SaveChangesAsync();



        }
    }
}
