using CQRSRentACar.Context;
using CQRSRentACar.CQRSPattern.Commands.MessageCommand;
using CQRSRentACar.CQRSPattern.Commands.ServiceCommand;

namespace CQRSRentACar.CQRSPattern.Handlers.ServiceHandlers
{
    public class UpdateServiceCommandHandler
    {
        private readonly CarContext _contex;

        public UpdateServiceCommandHandler(CarContext contex)
        {
            _contex = contex;
        }

        public async Task Handle(UpdateServiceCommand command)
        {
            var value = await _contex.Services.FindAsync(command.ServiceId);

            value.Title = command.Title;
            value.Description = command.Description;
            value.IconUrl = command.IconUrl;




            await _contex.SaveChangesAsync();



        }
    }
}
