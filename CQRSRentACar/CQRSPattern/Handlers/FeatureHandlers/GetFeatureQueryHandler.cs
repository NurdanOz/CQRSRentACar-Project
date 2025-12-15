using CQRSRentACar.Context;
using CQRSRentACar.CQRSPattern.Results.FeatureResults;
using Microsoft.EntityFrameworkCore;

namespace CQRSRentACar.CQRSPattern.Handlers.FeatureHandlers
{
    public class GetFeatureQueryHandler
    {
        private readonly CarContext _context;

        public GetFeatureQueryHandler(CarContext context)
        {
            _context = context;
        }

        public async Task<List<GetFeatureQueryResult>> Handle()

        {
            var values = await _context.Features.AsNoTracking().ToListAsync();
            return values.Select(x => new GetFeatureQueryResult
            {
                FeatureId = x.FeatureId,
                Title = x.Title,
                Title2 = x.Title2,
                Description = x.Description,
                Description2 = x.Description2,
                ImageURL = x.ImageURL,
                Icon = x.Icon
            }).ToList();
        }
    }
}
