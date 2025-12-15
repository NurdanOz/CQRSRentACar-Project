using CQRSRentACar.Context;
using CQRSRentACar.CQRSPattern.Commands.MessageCommand;
using CQRSRentACar.CQRSPattern.Commands.ServiceCommand;

namespace CQRSRentACar.CQRSPattern.Handlers.ServiceHandlers
{
    public class RemoveServiceCommandHandler
    {
        private readonly CarContext _context;

        public RemoveServiceCommandHandler(CarContext context)
        {
            _context = context;
        }

        public async Task Handle(RemoveServiceCommand command)
        {
            var value = await _context.Services.FindAsync(command.ServiceId);

            _context.Services.Remove(value);

            await _context.SaveChangesAsync();
        }
    }
}
