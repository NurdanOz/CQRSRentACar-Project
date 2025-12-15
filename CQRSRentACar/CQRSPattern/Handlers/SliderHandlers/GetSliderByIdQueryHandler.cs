using CQRSRentACar.Context;
using CQRSRentACar.CQRSPattern.Queries.ServiceQueries;
using CQRSRentACar.CQRSPattern.Queries.SliderQueries;
using CQRSRentACar.CQRSPattern.Results.ServiceResults;
using CQRSRentACar.CQRSPattern.Results.SliderResults;
using Microsoft.EntityFrameworkCore;

namespace CQRSRentACar.CQRSPattern.Handlers.SliderHandlers
{
    public class GetSliderByIdQueryHandler
    {
        private readonly CarContext _context;

        public GetSliderByIdQueryHandler(CarContext context)
        {
            _context = context;
        }

        public async Task<GetSliderByIdQueryResult> Handle(GetSliderByIdQuery query)
        {
            var value = await _context.Sliders.AsNoTracking().FirstOrDefaultAsync(x => x.SliderId == query.Id);
            return new GetSliderByIdQueryResult
            {
                SliderId = value.SliderId,
                Title = value.Title,
                Description = value.Description,
                ImageUrl = value.ImageUrl




            };
        }
    }
}