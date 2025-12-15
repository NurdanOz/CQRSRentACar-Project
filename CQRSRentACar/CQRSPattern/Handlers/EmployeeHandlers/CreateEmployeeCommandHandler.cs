using CQRSRentACar.Context;
using CQRSRentACar.CQRSPattern.Commands.CarCommand;
using CQRSRentACar.CQRSPattern.Commands.EmployeeCommand;

namespace CQRSRentACar.CQRSPattern.Handlers.EmployeeHandlers
{
    public class CreateEmployeeCommandHandler
    {
        private readonly CarContext _context;

        public CreateEmployeeCommandHandler(CarContext context)
        {
            _context = context;
        }

       public async Task Handle(CreateEmployeeCommand command)
        {
            _context.Employees.Add(new Entities.Employee
            {
                NameSurname = command.NameSurname,
                Profession = command.Profession,
                ImageUrl = command.ImageUrl
            });
            await _context.SaveChangesAsync();
        }

    }
}
