using CQRSRentACar.Context;
using CQRSRentACar.CQRSPattern.Queries.BookingQueries;
using CQRSRentACar.CQRSPattern.Results.BookingResults;
using Microsoft.EntityFrameworkCore;

namespace CQRSRentACar.CQRSPattern.Handlers.BookingHandlers
{
    public class GetBookingQueryHandler
    {
        private readonly CarContext _context;

        public GetBookingQueryHandler(CarContext context)
        {
            _context = context;
        }

        public async Task<List<GetBookingQueryResult>> Handle()
        {
            var values = await _context.Bookings
                .Include(x => x.Car)
                .Select(x => new GetBookingQueryResult
                {
                    BookingId = x.BookingId,
                    CarId = x.CarId,
                    Name = x.Name,
                    Email = x.Email,
                    Phone = x.Phone,
                    PickUpDate = x.PickUpDate,
                    DropOffDate = x.DropOffDate,
                    PickUpLocation = x.PickUpLocation,
                    DropOffLocation = x.DropOffLocation,
                    CarBrand = x.CarBrand,
                    CarModel = x.CarModel,
                    Status = x.Status,
                    TotalPrice = x.TotalPrice
                })
                .ToListAsync();

            return values;
        }
    }
}