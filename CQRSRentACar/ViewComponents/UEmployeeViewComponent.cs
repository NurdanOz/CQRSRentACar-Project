using CQRSRentACar.CQRSPattern.Handlers.EmployeeHandlers;
using Microsoft.AspNetCore.Mvc;

namespace CQRSRentACar.ViewComponents
{
    public class UEmployeeViewComponent : ViewComponent
    {
        private readonly GetEmployeeQueryHandler _handler;

        public UEmployeeViewComponent(GetEmployeeQueryHandler handler)
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