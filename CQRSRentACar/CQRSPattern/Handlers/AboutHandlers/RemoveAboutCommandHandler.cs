using CQRSRentACar.Context;
using CQRSRentACar.CQRSPattern.Commands.AboutCommand;

namespace CQRSRentACar.CQRSPattern.Handlers.AboutHandlers
{
    public class RemoveAboutCommandHandler
    {
        private readonly CarContext _context;

        public RemoveAboutCommandHandler(CarContext context)
        {
            _context = context;
        }
        
        public async Task Handle(RemoveAboutCommand command)
        {
            var value = await _context.Abouts.FindAsync(command.AboutId);
            _context.Abouts.Remove(value);
            await _context.SaveChangesAsync();
        }

        
        
    }
}
