using CQRSRentACar.Context;
using CQRSRentACar.CQRSPattern.Commands.EmployeeCommand;

namespace CQRSRentACar.CQRSPattern.Handlers.EmployeeHandlers
{
    public class UpdateEmployeeCommandHandler
    {
        private readonly CarContext _contex;

        public UpdateEmployeeCommandHandler(CarContext contex)
        {
            _contex = contex;
        }

        public async Task Handle(UpdateEmployeeCommand command)
        {
            var value = await _contex.Employees.FindAsync(command.EmployeeId);

           
                value.NameSurname = command.NameSurname;
                value.Profession = command.Profession;
                value.ImageUrl = command.ImageUrl;
                await _contex.SaveChangesAsync();
               
        }
    }
}
