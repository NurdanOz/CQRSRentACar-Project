using CQRSRentACar.CQRSPattern.Handlers.FeatureHandlers;
using Microsoft.AspNetCore.Mvc;

namespace CQRSRentACar.ViewComponents
{
    public class UFeaturesViewComponent : ViewComponent
    {
        private readonly GetFeatureQueryHandler _handler;

        public UFeaturesViewComponent(GetFeatureQueryHandler handler)
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
