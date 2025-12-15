using CQRSRentACar.Context;
using CQRSRentACar.CQRSPattern.Commands.AboutCommand;

namespace CQRSRentACar.CQRSPattern.Handlers.AboutHandlers
{
    public class CreateAboutCommandHandler
    {
        private readonly CarContext _context;

        public CreateAboutCommandHandler(CarContext context)
        {
            _context = context;
        }

        public async Task Handle(CreateAboutCommand command)
        {
            _context.Abouts.Add(new Entities.About
            {
                Title = command.Title,
                Description = command.Description,
                Description2 = command.Description2,
            });
            await _context.SaveChangesAsync();

        }
    }
}
