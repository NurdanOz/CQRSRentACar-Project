using CQRSRentACar.Context;
using CQRSRentACar.CQRSPattern.Commands.LocationCommand;
using CQRSRentACar.CQRSPattern.Commands.MessageCommand;

namespace CQRSRentACar.CQRSPattern.Handlers.MessageHandlers
{
    public class RemoveMessageCommandHandler
    {
        private readonly CarContext _context;

        public RemoveMessageCommandHandler(CarContext context)
        {
            _context = context;
        }

        public async Task Handle(RemoveMesageCommand command)
        {
            var value = await _context.Messages.FindAsync(command.MessageId);

            _context.Messages.Remove(value);

            await _context.SaveChangesAsync();
        }
    }
}
