using CQRSRentACar.Context;
using CQRSRentACar.CQRSPattern.Commands.LocationCommand;
using CQRSRentACar.CQRSPattern.Commands.MessageCommand;

namespace CQRSRentACar.CQRSPattern.Handlers.MessageHandlers
{
    public class CreateMessageCommandHandler
    {
        private readonly CarContext _context;

        public CreateMessageCommandHandler(CarContext context)
        {
            _context = context;
        }

        public async Task Handle(CreateMessageCommand command)
        {
            _context.Messages.Add(new Entities.Message
            {
                Name = command.Name,
                FullName=command.FullName,
                Surname = command.Surname,
                Email = command.Email,
                Subject = command.Subject,
                MessageDetail = command.MessageDetail,
                Phone = command.Phone,
                
               



            });

            await _context.SaveChangesAsync();
        }
    }
}
