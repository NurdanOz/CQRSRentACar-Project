using CQRSRentACar.Context;
using CQRSRentACar.CQRSPattern.Queries.EmployeeQueries;
using CQRSRentACar.CQRSPattern.Results.EmployeeResults;
using Microsoft.EntityFrameworkCore;

namespace CQRSRentACar.CQRSPattern.Handlers.EmployeeHandlers
{
    public class GetEmployeeByIdQueryHandler
    {
        private readonly CarContext _context;

        public GetEmployeeByIdQueryHandler(CarContext context)
        {
            _context = context;
        }

        public async Task<GetEmployeeByIdQueryResult> Handle(GetEmployeeByIdQuery query)
        {
            var value = await _context.Employees.AsNoTracking().FirstOrDefaultAsync(x => x.EmployeeId == query.Id);

            return new GetEmployeeByIdQueryResult
            {
                EmployeeId = value.EmployeeId,
                NameSurname = value.NameSurname,
                Profession = value.Profession,
                ImageUrl = value.ImageUrl
            };
        }
    }
}
