using CQRSRentACar.Context;
using CQRSRentACar.CQRSPattern.Commands.MessageCommand;
using CQRSRentACar.CQRSPattern.Commands.ServiceCommand;

namespace CQRSRentACar.CQRSPattern.Handlers.ServiceHandlers
{
    public class CreateServiceCommandHandler
    {
        private readonly CarContext _context;

        public CreateServiceCommandHandler(CarContext context)
        {
            _context = context;
        }

        public async Task Handle(CreateServiceCommand command)
        {
            _context.Services.Add(new Entities.Service
            {
                Title = command.Title,
                Description = command.Description,
                IconUrl = command.IconUrl



            });

            await _context.SaveChangesAsync();
        }
    }
}
