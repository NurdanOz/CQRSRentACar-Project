using CQRSRentACar.Context;
using CQRSRentACar.CQRSPattern.Queries.MessageQueries;
using CQRSRentACar.CQRSPattern.Queries.ServiceQueries;
using CQRSRentACar.CQRSPattern.Results.MessageResults;
using CQRSRentACar.CQRSPattern.Results.ServiceResults;
using Microsoft.EntityFrameworkCore;

namespace CQRSRentACar.CQRSPattern.Handlers.ServiceHandlers
{
    public class GetServiceByIdQueryHandler
    {
        private readonly CarContext _context;

        public GetServiceByIdQueryHandler(CarContext context)
        {
            _context = context;
        }

        public async Task<GetServiceByIdQueryResult> Handle(GetServiceByIdQuery query)
        {
            var value = await _context.Services.AsNoTracking().FirstOrDefaultAsync(x => x.ServiceId == query.Id);
            return new GetServiceByIdQueryResult
            {
                ServiceId = value.ServiceId,
                Title = value.Title,
                Description = value.Description,
                IconUrl = value.IconUrl



            };
        }
    }
}