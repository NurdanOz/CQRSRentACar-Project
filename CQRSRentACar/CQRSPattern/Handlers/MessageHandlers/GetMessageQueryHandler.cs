using CQRSRentACar.Context;
using CQRSRentACar.CQRSPattern.Results.LocationResults;
using CQRSRentACar.CQRSPattern.Results.MessageResults;
using Microsoft.EntityFrameworkCore;

namespace CQRSRentACar.CQRSPattern.Handlers.MessageHandlers
{
    public class GetMessageQueryHandler
    {
        private readonly CarContext _context;

        public GetMessageQueryHandler(CarContext context)
        {
            _context = context;
        }

        public async Task<List<GetMessageQueryResult>> Handle()

        {
            var values = await _context.Messages.AsNoTracking().ToListAsync();
            return values.Select(x => new GetMessageQueryResult
            {
                MessageId = x.MessageId,
                Name = x.Name,
                FullName=x.FullName,
                Surname = x.Surname,
                Email = x.Email,
                Subject = x.Subject,
                MessageDetail = x.MessageDetail,
                Phone = x.Phone


            }).ToList();
        }
    }

}