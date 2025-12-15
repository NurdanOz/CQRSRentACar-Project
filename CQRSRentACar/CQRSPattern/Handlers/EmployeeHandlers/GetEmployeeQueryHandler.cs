using CQRSRentACar.Context;
using CQRSRentACar.CQRSPattern.Results.EmployeeResults;
using Microsoft.EntityFrameworkCore;

namespace CQRSRentACar.CQRSPattern.Handlers.EmployeeHandlers
{
    public class GetEmployeeQueryHandler
    {
        private readonly CarContext _context;

        public GetEmployeeQueryHandler(CarContext context)
        {
            _context = context;
        }

        public async Task<List<GetEmployeeQueryResult>> Handle()
        {
            var values = await _context.Employees
                .AsNoTracking()
                .Take(4) 
                .ToListAsync();

            return values.Select(x => new GetEmployeeQueryResult
            {
                EmployeeId = x.EmployeeId,
                NameSurname = x.NameSurname,
                Profession = x.Profession,
                ImageUrl = x.ImageUrl
            }).ToList();
        }
    }
}