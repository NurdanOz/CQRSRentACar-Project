using CQRSRentACar.Context;
using CQRSRentACar.CQRSPattern.Results.ServiceResults;
using CQRSRentACar.CQRSPattern.Results.SliderResults;
using Microsoft.EntityFrameworkCore;

namespace CQRSRentACar.CQRSPattern.Handlers.SliderHandlers
{
    public class GetSliderQueryHandler
    {
        private readonly CarContext _context;

        public GetSliderQueryHandler(CarContext context)
        {
            _context = context;
        }

        public async Task<List<GetSliderQueryResult>> Handle()

        {
            var values = await _context.Sliders.AsNoTracking().ToListAsync();
            return values.Select(x => new GetSliderQueryResult
            {
                SliderId = x.SliderId,
                Title = x.Title,
                Description = x.Description,
                ImageUrl = x.ImageUrl




            }).ToList();
        }
    }
}
