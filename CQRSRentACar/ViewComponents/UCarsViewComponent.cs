using CQRSRentACar.CQRSPattern.Handlers.CarHandlers;
using Microsoft.AspNetCore.Mvc;

namespace CQRSRentACar.ViewComponents
{
    public class UCarsViewComponent : ViewComponent
    {
        private readonly GetCarQueryHandler _handler;

        public UCarsViewComponent(GetCarQueryHandler handler)
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