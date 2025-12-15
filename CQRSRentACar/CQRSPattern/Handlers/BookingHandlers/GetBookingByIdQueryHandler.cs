using CQRSRentACar.Context;
using CQRSRentACar.CQRSPattern.Queries.BookingQueries;
using CQRSRentACar.CQRSPattern.Results.BookingResults;
using Microsoft.EntityFrameworkCore;

namespace CQRSRentACar.CQRSPattern.Handlers.BookingHandlers
{
    public class GetBookingByIdQueryHandler
    {
        private readonly CarContext _context;

        public GetBookingByIdQueryHandler(CarContext context)
        {
            _context = context;
        }

        public async Task<GetBookingByIdQueryResult> Handle(GetBookingByIdQuery query)
        {
            var value = await _context.Bookings
                .Include(x => x.Car)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.BookingId == query.Id);

            return new GetBookingByIdQueryResult
            {
                BookingId = value.BookingId,
                CarId = value.CarId,
                Name = value.Name,
                Email = value.Email,
                Phone = value.Phone,
                PickUpDate = value.PickUpDate,
                DropOffDate = value.DropOffDate,
                PickUpLocation = value.PickUpLocation,
                DropOffLocation = value.DropOffLocation,
                CarBrand = value.CarBrand,
                CarModel = value.CarModel,
                Status = value.Status,
                TotalPrice = value.TotalPrice,
                CreatedDate = value.CreatedDate
            };
        }
    }
}