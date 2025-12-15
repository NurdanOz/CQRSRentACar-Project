using CQRSRentACar.Context;
using CQRSRentACar.CQRSPattern.Commands.EmployeeCommand;

namespace CQRSRentACar.CQRSPattern.Handlers.EmployeeHandlers
{
    public class RemoveEmployeeCommandHandler
    {

        private readonly CarContext _context;

        public RemoveEmployeeCommandHandler(CarContext context)
        {
            _context = context;
        }

        public async Task Handle(RemoveEmployeeCommand command)
        {
            var value = await _context.Employees.FindAsync(command.EmployeeId);

            _context.Employees.Remove(value);

            await _context.SaveChangesAsync();
        }
    }
}
