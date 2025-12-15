using CQRSRentACar.Context;
using CQRSRentACar.CQRSPattern.Queries.LocationQueries;
using CQRSRentACar.CQRSPattern.Queries.MessageQueries;
using CQRSRentACar.CQRSPattern.Results.LocationResults;
using CQRSRentACar.CQRSPattern.Results.MessageResults;
using Microsoft.EntityFrameworkCore;

namespace CQRSRentACar.CQRSPattern.Handlers.MessageHandlers
{
    public class GetMessageByIdQueryHandler
    {
        private readonly CarContext _context;

        public GetMessageByIdQueryHandler(CarContext context)
        {
            _context = context;
        }

        public async Task<GetMessageByIdQueryResult> Handle(GetMessageByIdQuery query)
        {
            var value = await _context.Messages.AsNoTracking().FirstOrDefaultAsync(x => x.MessageId == query.Id);
            return new GetMessageByIdQueryResult
            {
                MessageId = value.MessageId,
                Name = value.Name,
                FullName = value.FullName,
                Surname = value.Surname,
                Email = value.Email,
                Subject = value.Subject,
                MessageDetail = value.MessageDetail,
                Phone = value.Phone



            };
        }
    }
}