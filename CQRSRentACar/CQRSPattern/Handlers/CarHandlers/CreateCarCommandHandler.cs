using CQRSRentACar.Context;
using CQRSRentACar.CQRSPattern.Commands.CarCommand;

namespace CQRSRentACar.CQRSPattern.Handlers.CarHandlers
{
    public class CreateCarCommandHandler
    {
        private readonly CarContext _context;

        public CreateCarCommandHandler(CarContext context)
        {
            _context = context;
        }

        public async Task Handle(CreateCarCommand command)
        {
            _context.Cars.Add(new Entities.Car
            {
                CarBrand = command.CarBrand,
                CarModel = command.CarModel,
                CarImage = command.CarImage,
                CarSeat = command.CarSeat,
                CarTransmission = command.CarTransmission,
                CarFuel = command.CarFuel,
                CarYear = command.CarYear,
                CarKm = command.CarKm,
                CarReview = command.CarReview,
                CarPrice = command.CarPrice

            });

            await _context.SaveChangesAsync();
        }
    }
}
