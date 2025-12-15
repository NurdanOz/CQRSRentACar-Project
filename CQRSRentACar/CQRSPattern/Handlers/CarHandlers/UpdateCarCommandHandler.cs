using CQRSRentACar.Context;
using CQRSRentACar.CQRSPattern.Commands.CarCommand;

namespace CQRSRentACar.CQRSPattern.Handlers.CarHandlers
{
    public class UpdateCarCommandHandler
    {
        private readonly CarContext _context;

        public UpdateCarCommandHandler(CarContext context)
        {
            _context = context;
        }

        public async Task Handle(UpdateCarCommand command)
        {
            var value = await _context.Cars.FindAsync(command.CarId);

           
            value.CarImage = command.CarImage;
            value.CarBrand = command.CarBrand;
            value.CarModel = command.CarModel;
            value.CarSeat = command.CarSeat;
            value.CarTransmission = command.CarTransmission;
            value.CarFuel = command.CarFuel;
            value.CarYear = command.CarYear;
            value.CarKm = command.CarKm;
            value.CarReview = command.CarReview;
            value.CarPrice = command.CarPrice;


            await _context.SaveChangesAsync();
        }
    }
}
