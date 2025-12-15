
using Microsoft.AspNetCore.Mvc;

namespace CQRSRentACar.ViewComponents
{
    public class UNavbarViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}