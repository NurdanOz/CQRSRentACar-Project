using CQRSRentACar.Context;
using CQRSRentACar.CQRSPattern.Commands.FeatureCommand;
using CQRSRentACar.CQRSPattern.Commands.LocationCommand;

namespace CQRSRentACar.CQRSPattern.Handlers.LocationHandlers
{
    public class CreateLocationCommandHandler
    {
        private readonly CarContext _context;

        public CreateLocationCommandHandler(CarContext context)
        {
            _context = context;
        }

        public async Task Handle(CreateLocationCommand command)
        {
            _context.Locations.Add(new Entities.Location
            {
                LocationName = command.LocationName,
                Country = command.Country,
                City = command.City,
                CountryCode = command.CountryCode,
                CityCode = command.CityCode,
                Latitude = command.Latitude,
                Longitude = command.Longitude


            });

            await _context.SaveChangesAsync();
        }
    }
}
