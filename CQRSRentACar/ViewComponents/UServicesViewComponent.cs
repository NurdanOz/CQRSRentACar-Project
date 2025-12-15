using CQRSRentACar.CQRSPattern.Handlers.ServiceHandlers;
using Microsoft.AspNetCore.Mvc;

namespace CQRSRentACar.ViewComponents
{
    public class UServicesViewComponent : ViewComponent
    {
        private readonly GetServiceQueryHandler _handler;

        public UServicesViewComponent(GetServiceQueryHandler handler)
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