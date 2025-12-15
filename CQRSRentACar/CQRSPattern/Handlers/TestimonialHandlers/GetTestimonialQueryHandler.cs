using CQRSRentACar.Context;
using CQRSRentACar.CQRSPattern.Results.SliderResults;
using CQRSRentACar.CQRSPattern.Results.TestimonialResult;
using Microsoft.EntityFrameworkCore;

namespace CQRSRentACar.CQRSPattern.Handlers.TestimonialHandlers
{
    public class GetTestimonialQueryHandler
    {
        private readonly CarContext _context;

        public GetTestimonialQueryHandler(CarContext context)
        {
            _context = context;
        }

        public async Task<List<GetTestimonialQueryResult>> Handle()

        {
            var values = await _context.Testimonials.AsNoTracking().ToListAsync();
            return values.Select(x => new GetTestimonialQueryResult
            {
                TestimonialId = x.TestimonialId,
                NameSurname = x.NameSurname,
                Profession = x.Profession,
                Score = x.Score,
                Comment = x.Comment,
                ImageUrl = x.ImageUrl


            }).ToList();
        }
    }
}
