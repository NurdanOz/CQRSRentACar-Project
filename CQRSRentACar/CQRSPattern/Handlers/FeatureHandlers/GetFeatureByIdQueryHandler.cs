using CQRSRentACar.Context;
using CQRSRentACar.CQRSPattern.Queries.FeatureQueries;
using CQRSRentACar.CQRSPattern.Results.FeatureResults;
using Microsoft.EntityFrameworkCore;

namespace CQRSRentACar.CQRSPattern.Handlers.FeatureHandlers
{
    public class GetFeatureByIdQueryHandler
    {
        private readonly CarContext _context;

        public GetFeatureByIdQueryHandler(CarContext context)
        {
            _context = context;
        }

        public async Task<GetFeatureByIdQueryResult> Handle(GetFeatureByIdQuery query)
        {
            var value = await _context.Features.AsNoTracking().FirstOrDefaultAsync(x => x.FeatureId == query.Id);
            return new GetFeatureByIdQueryResult
            {
                FeatureId = value.FeatureId,
                Title = value.Title,
                Title2 = value.Title2,
                Description = value.Description,
                Description2 = value.Description2,
                ImageURL = value.ImageURL,
                Icon = value.Icon


            };
        }
    }
}
