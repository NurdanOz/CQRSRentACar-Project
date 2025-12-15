using CQRSRentACar.Context;
using CQRSRentACar.CQRSPattern.Commands.LocationCommand;
using CQRSRentACar.CQRSPattern.Commands.MessageCommand;

namespace CQRSRentACar.CQRSPattern.Handlers.MessageHandlers
{
    public class UpdateMessageCommandHandler
    {
        private readonly CarContext _contex;

        public UpdateMessageCommandHandler(CarContext contex)
        {
            _contex = contex;
        }

        public async Task Handle(UpdateMessageCommand command)
        {
            var value = await _contex.Messages.FindAsync(command.MessageId);

           value.Name = command.Name;
            value.FullName = command.FullName;
            value.Surname = command.Surname;
            value.Email = command.Email;
            value.Subject = command.Subject;
            value.MessageDetail = command.MessageDetail;
            value.Phone = command.Phone;



            await _contex.SaveChangesAsync();



        }
    }
}
