using CQRSRentACar.Context;
using CQRSRentACar.CQRSPattern.Results.MessageResults;
using CQRSRentACar.CQRSPattern.Results.ServiceResults;
using Microsoft.EntityFrameworkCore;

namespace CQRSRentACar.CQRSPattern.Handlers.ServiceHandlers
{
    public class GetServiceQueryHandler
    {
        private readonly CarContext _context;

        public GetServiceQueryHandler(CarContext context)
        {
            _context = context;
        }

        public async Task<List<GetServiceQueryResult>> Handle()

        {
            var values = await _context.Services.AsNoTracking().ToListAsync();
            return values.Select(x => new GetServiceQueryResult
            {
                ServiceId = x.ServiceId,
                Title = x.Title,
                Description = x.Description,
                IconUrl = x.IconUrl



            }).ToList();
        }
    }

}