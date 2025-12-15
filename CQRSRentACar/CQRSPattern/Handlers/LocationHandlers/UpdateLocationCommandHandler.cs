using CQRSRentACar.Context;
using CQRSRentACar.CQRSPattern.Commands.FeatureCommand;
using CQRSRentACar.CQRSPattern.Commands.LocationCommand;

namespace CQRSRentACar.CQRSPattern.Handlers.LocationHandlers
{
    public class UpdateLocationCommandHandler
    {
        private readonly CarContext _contex;

        public UpdateLocationCommandHandler(CarContext contex)
        {
            _contex = contex;
        }

        public async Task Handle(UpdateLocationCommand command)
        {
            var value = await _contex.Locations.FindAsync(command.LocationId);

            value.LocationName = command.LocationName;
            value.Country = command.Country;
            value.City = command.City;
            value.CountryCode = command.CountryCode;
            value.CityCode = command.CityCode;
            value.Latitude = command.Latitude;
            value.Longitude = command.Longitude;


            await _contex.SaveChangesAsync();



        }
    }
}
