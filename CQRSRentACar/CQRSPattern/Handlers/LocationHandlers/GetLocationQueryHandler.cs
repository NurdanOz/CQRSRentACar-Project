using CQRSRentACar.Context;
using CQRSRentACar.CQRSPattern.Results.FeatureResults;
using CQRSRentACar.CQRSPattern.Results.LocationResults;
using Microsoft.EntityFrameworkCore;

namespace CQRSRentACar.CQRSPattern.Handlers.LocationHandlers
{
    public class GetLocationQueryHandler
    {
        private readonly CarContext _context;

        public GetLocationQueryHandler(CarContext context)
        {
            _context = context;
        }

        public async Task<List<GetLocationQueryResult>> Handle()

        {
            var values = await _context.Locations.AsNoTracking().ToListAsync();
            return values.Select(x => new GetLocationQueryResult
            {
               LocationId = x.LocationId,
                LocationName = x.LocationName,
                Country = x.Country,
                City = x.City,
                CountryCode = x.CountryCode,
                CityCode = x.CityCode,
                Latitude = x.Latitude,
                Longitude = x.Longitude

            }).ToList();
        }
    }
}
