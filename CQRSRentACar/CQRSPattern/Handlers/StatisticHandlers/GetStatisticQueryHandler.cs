using CQRSRentACar.CQRSPattern.Queries.StatisticQueries;
using CQRSRentACar.CQRSPattern.Results.StatisticResults;
using CQRSRentACar.Context;

namespace CQRSRentACar.CQRSPattern.Handlers.StatisticHandlers
{
    public class GetStatisticQueryHandler
    {
        private readonly CarContext _context;

        public GetStatisticQueryHandler(CarContext context)
        {
            _context = context;
        }

        public GetStatisticQueryResult Handle(GetStatisticQuery query)
        {
            int carCount = _context.Cars.Count();
            int employeeCount = _context.Employees.Count();
            int serviceCount = _context.Services.Count();
            int bookingCount = _context.Bookings.Count();

            return new GetStatisticQueryResult
            {
                CarCount = carCount,
                EmployeeCount = employeeCount,
                ServiceCount = serviceCount,
                BookingCount = bookingCount
            };
        }
    }
}