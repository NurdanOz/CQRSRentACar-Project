using Microsoft.EntityFrameworkCore;
using CQRSRentACar.Context;
using CQRSRentACar.CQRSPattern.Queries.BookingQueries;
using CQRSRentACar.CQRSPattern.Results.BookingResults;

namespace CQRSRentACar.CQRSPattern.Handlers.BookingHandlers
{
    public class GetAllBookingsQueryHandler
    {
        private readonly CarContext _context;

        public GetAllBookingsQueryHandler(CarContext context)
        {
            _context = context;
        }

        public async Task<List<GetAllBookingsQueryResult>> Handle(GetAllBookingsQuery query)
        {
            return await _context.Bookings
                .Include(x => x.Car)
                .Select(x => new GetAllBookingsQueryResult
                {
                    BookingId = x.BookingId,
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
                    TotalPrice = x.TotalPrice,
                    CreatedDate = x.CreatedDate
                })
                .OrderByDescending(x => x.CreatedDate)
                .ToListAsync();
        }
    }
}