using CQRSRentACar.Context;
using CQRSRentACar.CQRSPattern.Queries.FeatureQueries;
using CQRSRentACar.CQRSPattern.Queries.LocationQueries;
using CQRSRentACar.CQRSPattern.Results.FeatureResults;
using CQRSRentACar.CQRSPattern.Results.LocationResults;
using Microsoft.EntityFrameworkCore;

namespace CQRSRentACar.CQRSPattern.Handlers.LocationHandlers
{
    public class GetLocationByIdQueryHandler
    {
        private readonly CarContext _context;

        public GetLocationByIdQueryHandler(CarContext context)
        {
            _context = context;
        }

        public async Task<GetLocationByIdQueryResult> Handle(GetLocationByIdQuery query)
        {
            var value = await _context.Locations.AsNoTracking().FirstOrDefaultAsync(x => x.LocationId == query.Id);
            return new GetLocationByIdQueryResult
            {
                LocationId = value.LocationId,
                LocationName = value.LocationName,
                Country = value.Country,
                City = value.City,
                CountryCode = value.CountryCode,
                CityCode = value.CityCode,
                Latitude = value.Latitude,
                Longitude = value.Longitude



            };
        }
    }

}