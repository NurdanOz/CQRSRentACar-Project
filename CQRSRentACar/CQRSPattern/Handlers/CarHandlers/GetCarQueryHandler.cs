using CQRSRentACar.Context;
using CQRSRentACar.CQRSPattern.Results.CarResults;
using Microsoft.EntityFrameworkCore;

namespace CQRSRentACar.CQRSPattern.Handlers.CarHandlers
{
    public class GetCarQueryHandler
    {
        private readonly CarContext _context;

        public GetCarQueryHandler(CarContext context)
        {
            _context = context;
        }

        public async Task<List<GetCarQueryResult>> Handle()
        {
            var values = await _context.Cars.AsNoTracking().ToListAsync();

            return values.Select(x => new GetCarQueryResult
            {
                CarId = x.CarId,
                CarImage = x.CarImage,
                CarBrand = x.CarBrand,
                CarModel = x.CarModel,
                CarSeat = x.CarSeat,
                CarTransmission = x.CarTransmission,
                CarFuel = x.CarFuel,
                CarYear = x.CarYear,
                CarKm = x.CarKm,
                CarReview = x.CarReview,
                CarPrice = x.CarPrice


            }).ToList();
        }
    }
}
