using CQRSRentACar.Context;
using CQRSRentACar.CQRSPattern.Results.AboutResults;
using Microsoft.EntityFrameworkCore;

namespace CQRSRentACar.CQRSPattern.Handlers.AboutHandlers
{
    public class GetAboutQueryHandler
    {
        private readonly CarContext _context;

        public GetAboutQueryHandler(CarContext context)
        {
            _context = context;
        }

        public async Task<List<GetAboutQueryResult>> Handle()
        {
            var values = await _context.Abouts.AsNoTracking().ToListAsync();
            return values.Select(x => new GetAboutQueryResult
            {
                AboutId = x.AboutId,
                Title = x.Title,
                Description = x.Description,
                Description2 = x.Description2,

            }).ToList();
        }
    }
}
