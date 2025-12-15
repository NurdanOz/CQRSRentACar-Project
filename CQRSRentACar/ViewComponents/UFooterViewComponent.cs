using Microsoft.AspNetCore.Mvc;

namespace CQRSRentACar.ViewComponents
{
    public class UFooterViewComponent:ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
