using CQRSRentACar.Context;
using CQRSRentACar.CQRSPattern.Commands.CarCommand;

namespace CQRSRentACar.CQRSPattern.Handlers.CarHandlers
{
    public class RemoveCarCommandHandler
    {
        private readonly CarContext _context;

        public RemoveCarCommandHandler(CarContext context)
        {
            _context = context;
        }

        public async Task Handle(RemoveCarCommand command)
        {
            var value = await _context.Cars.FindAsync(command.CarId);
            _context.Cars.Remove(value);
            await _context.SaveChangesAsync();
        }
    }
}
