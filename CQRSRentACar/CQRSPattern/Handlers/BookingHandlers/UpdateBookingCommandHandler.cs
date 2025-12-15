using CQRSRentACar.Context;
using CQRSRentACar.CQRSPattern.Commands.BookingCommand;

namespace CQRSRentACar.CQRSPattern.Handlers.BookingHandlers
{
    public class UpdateBookingCommandHandler
    {
        private readonly CarContext _context;

        public UpdateBookingCommandHandler(CarContext context)
        {
            _context = context;
        }

        public async Task Handle(UpdateBookingCommand command)
        {
            var value = await _context.Bookings.FindAsync(command.BookingId);

            value.Name = command.Name;
            value.Email = command.Email;
            value.Phone = command.Phone;
            value.PickUpDate = command.PickUpDate;
            value.DropOffDate = command.DropOffDate;
            value.PickUpLocation = command.PickUpLocation;
            value.DropOffLocation = command.DropOffLocation;
            value.CarId = command.CarId;
            value.CarBrand = command.CarBrand;
            value.CarModel = command.CarModel;
            value.Status = command.Status;
            value.TotalPrice = command.TotalPrice;

            await _context.SaveChangesAsync();
        }
    }
}