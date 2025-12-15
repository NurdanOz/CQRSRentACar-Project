using CQRSRentACar.Context;
using CQRSRentACar.CQRSPattern.Commands.FeatureCommand;

namespace CQRSRentACar.CQRSPattern.Handlers.FeatureHandlers
{
    public class CreateFeatureCommandHandler
    {
        private readonly CarContext _context;

        public CreateFeatureCommandHandler(CarContext context)
        {
            _context = context;
        }

        public async Task Handle(CreateFeatureCommand command)
        {
            _context.Features.Add(new Entities.Feature
            {
                Title = command.Title,
                Title2 = command.Title2,
                Description = command.Description,
                Description2 = command.Description2,
                ImageURL = command.ImageURL,
                Icon = command.Icon
            });

            await _context.SaveChangesAsync();
        }
    }
}
