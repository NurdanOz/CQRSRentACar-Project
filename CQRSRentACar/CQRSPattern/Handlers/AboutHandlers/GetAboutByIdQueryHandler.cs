using CQRSRentACar.Context;
using CQRSRentACar.CQRSPattern.Queries.AboutQueries;
using CQRSRentACar.CQRSPattern.Results.AboutResults;
using Microsoft.EntityFrameworkCore;

namespace CQRSRentACar.CQRSPattern.Handlers.AboutHandlers
{
    public class GetAboutByIdQueryHandler
    {
        private readonly CarContext _context;

        public GetAboutByIdQueryHandler(CarContext context)
        {
            _context = context;
        }

        public async Task<GetAboutByIdQueryResult> Handle(GetAboutByIdQuery query)
        {
            var value = await _context.Abouts.AsNoTracking().FirstOrDefaultAsync(x=> x.AboutId == query.Id);

            return new GetAboutByIdQueryResult
            {
                AboutId = value.AboutId,
                Title = value.Title,
                Description = value.Description,
                Description2 = value.Description2,
            };
        }
    }
}
