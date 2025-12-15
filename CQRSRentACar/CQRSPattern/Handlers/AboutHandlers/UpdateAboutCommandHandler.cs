using CQRSRentACar.Context;
using CQRSRentACar.CQRSPattern.Commands.AboutCommand;

namespace CQRSRentACar.CQRSPattern.Handlers.AboutHandlers
{
    public class UpdateAboutCommandHandler
    {
        private readonly CarContext _context;

        public UpdateAboutCommandHandler(CarContext context)
        {
            _context = context;
        }

        public async Task Handle(UpdateAboutCommand command)
        {
            var value = await _context.Abouts.FindAsync(command.AboutId);

            value.Title = command.Title;
            value.Description = command.Description;
            value.Description2 = command.Description2;

            await _context.SaveChangesAsync();
        }
    }
}
