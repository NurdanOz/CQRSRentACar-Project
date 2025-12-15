using CQRSRentACar.Context;
using CQRSRentACar.CQRSPattern.Commands.BookingCommand;

namespace CQRSRentACar.CQRSPattern.Handlers.BookingHandlers
{
    public class RemoveBookingCommandHandler
    {
        private readonly CarContext _context;

        public RemoveBookingCommandHandler(CarContext context)
        {
            _context = context;
        }

        public async Task Handle (RemoveBookingCommand command)
        {
            var value = await _context.Bookings.FindAsync(command.BookingId);
            _context.Bookings.Remove(value);
            await _context.SaveChangesAsync();
        }
    }
}
