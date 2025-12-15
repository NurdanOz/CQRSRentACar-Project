using CQRSRentACar.Context;
using CQRSRentACar.CQRSPattern.Commands.EmployeeCommand;
using CQRSRentACar.CQRSPattern.Commands.FeatureCommand;

namespace CQRSRentACar.CQRSPattern.Handlers.FeatureHandlers
{
    public class RemoveFeatureCommandHandler
    {
        private readonly CarContext _context;

        public RemoveFeatureCommandHandler(CarContext context)
        {
            _context = context;
        }

        public async Task Handle(RemoveFeatureCommand command)
        {
            var value = await _context.Employees.FindAsync(command.FeatureId);

            _context.Employees.Remove(value);

            await _context.SaveChangesAsync();
        }
    }
}
