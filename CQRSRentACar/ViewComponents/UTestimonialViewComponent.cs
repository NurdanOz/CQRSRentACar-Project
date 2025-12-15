using CQRSRentACar.CQRSPattern.Handlers.TestimonialHandlers;
using Microsoft.AspNetCore.Mvc;

namespace CQRSRentACar.ViewComponents
{
    public class UTestimonialViewComponent : ViewComponent
    {
        private readonly GetTestimonialQueryHandler _handler;

        public UTestimonialViewComponent(GetTestimonialQueryHandler handler)
        {
            _handler = handler;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _handler.Handle();
            return View(values);
        }
    }
}