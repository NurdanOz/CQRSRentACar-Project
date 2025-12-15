using CQRSRentACar.CQRSPattern.Handlers.SliderHandlers;
using Microsoft.AspNetCore.Mvc;

namespace CQRSRentACar.ViewComponents
{
    public class USliderViewComponent : ViewComponent
    {
        private readonly GetSliderQueryHandler _handler;

        public USliderViewComponent(GetSliderQueryHandler handler)
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