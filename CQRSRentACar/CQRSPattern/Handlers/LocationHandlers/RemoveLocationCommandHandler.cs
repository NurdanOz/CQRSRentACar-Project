using CQRSRentACar.Context;
using CQRSRentACar.CQRSPattern.Commands.FeatureCommand;
using CQRSRentACar.CQRSPattern.Commands.LocationCommand;

namespace CQRSRentACar.CQRSPattern.Handlers.LocationHandlers
{
    public class RemoveLocationCommandHandler
    {
        private readonly CarContext _context;

        public RemoveLocationCommandHandler(CarContext context)
        {
            _context = context;
        }

        public async Task Handle(RemoveLocationCommand command)
        {
            var value = await _context.Locations.FindAsync(command.LocationId);

            _context.Locations.Remove(value);

            await _context.SaveChangesAsync();
        }
    }
}
