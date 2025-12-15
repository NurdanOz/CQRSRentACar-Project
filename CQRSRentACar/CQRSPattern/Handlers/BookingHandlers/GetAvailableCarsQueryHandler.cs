using CQRSRentACar.Context;
using CQRSRentACar.CQRSPattern.Queries.BookingQueries;
using CQRSRentACar.CQRSPattern.Results.BookingResults;
using Microsoft.EntityFrameworkCore;

namespace CQRSRentACar.CQRSPattern.Handlers.BookingHandlers
{
    public class GetAvailableCarsQueryHandler
    {
        private readonly CarContext _context;

        public GetAvailableCarsQueryHandler(CarContext context)
        {
            _context = context;
        }

        public async Task<List<GetAvailableCarsQueryResult>> Handle(GetAvailableCarsQuery query)
        {
            // ŞİMDİLİK TÜM ARAÇLARI GÖSTER (Rezervasyon kontrolü sonra eklenecek)
            var availableCars = await _context.Cars
                .Select(c => new GetAvailableCarsQueryResult
                {
                    CarId = c.CarId,
                    CarImage = c.CarImage,
                    CarBrand = c.CarBrand,
                    CarModel = c.CarModel,
                    CarSeat = c.CarSeat,
                    CarTransmission = c.CarTransmission,
                    CarFuel = c.CarFuel,
                    CarPrice = c.CarPrice,
                    CarReview = c.CarReview
                })
                .ToListAsync();

            return availableCars;
        }
    }
}