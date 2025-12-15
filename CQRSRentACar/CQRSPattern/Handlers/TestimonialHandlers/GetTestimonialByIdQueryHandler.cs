using CQRSRentACar.Context;
using CQRSRentACar.CQRSPattern.Queries.SliderQueries;
using CQRSRentACar.CQRSPattern.Queries.TestimonialQueries;
using CQRSRentACar.CQRSPattern.Results.SliderResults;
using CQRSRentACar.CQRSPattern.Results.TestimonialResult;
using Microsoft.EntityFrameworkCore;

namespace CQRSRentACar.CQRSPattern.Handlers.TestimonialHandlers
{
    public class GetTestimonialByIdQueryHandler
    {
        private readonly CarContext _context;

        public GetTestimonialByIdQueryHandler(CarContext context)
        {
            _context = context;
        }

        public async Task<GetTestimonialByIdQueryResult> Handle(GetTestimonialByIdQuery query)
        {
            var value = await _context.Testimonials.AsNoTracking().FirstOrDefaultAsync(x => x.TestimonialId == query.Id);
            return new GetTestimonialByIdQueryResult
            {
                TestimonialId = value.TestimonialId,
                NameSurname = value.NameSurname,
                Profession = value.Profession,
                Score = value.Score,
                Comment = value.Comment,
                ImageUrl = value.ImageUrl





            };
        }
    }

}