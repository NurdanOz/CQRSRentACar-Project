using CQRSRentACar.Context;
using CQRSRentACar.CQRSPattern.Queries.CarQueries;
using CQRSRentACar.CQRSPattern.Results.AboutResults;
using CQRSRentACar.CQRSPattern.Results.CarResults;
using Microsoft.EntityFrameworkCore;

namespace CQRSRentACar.CQRSPattern.Handlers.CarHandlers
{
    public class GetCarByIdQueryHandler
    {
        private readonly CarContext _context;

        public GetCarByIdQueryHandler(CarContext context)
        {
            _context = context;
        }

        public async Task<GetCarByIdQueryResult> Handle(GetCarByIdQuery query)
        {
            var value = await _context.Cars.AsNoTracking().FirstOrDefaultAsync(x => x.CarId == query.Id);

            return new GetCarByIdQueryResult
            {
                CarId = value.CarId,
                CarImage = value.CarImage,
                CarBrand = value.CarBrand,
                CarModel = value.CarModel,
                CarSeat = value.CarSeat,
                CarTransmission = value.CarTransmission,
                CarFuel = value.CarFuel,
                CarYear = value.CarYear,
                CarKm = value.CarKm,
                CarReview = value.CarReview,
                CarPrice = value.CarPrice
            };
        }

    }
}
