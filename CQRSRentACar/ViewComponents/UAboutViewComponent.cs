using CQRSRentACar.CQRSPattern.Handlers.AboutHandlers;
using CQRSRentACar.CQRSPattern.Queries.AboutQueries;
using Microsoft.AspNetCore.Mvc;

namespace CQRSRentACar.ViewComponents
{

    public class UAboutViewComponent : ViewComponent
    {
        private readonly GetAboutByIdQueryHandler _handler;

        public UAboutViewComponent(GetAboutByIdQueryHandler handler)
        {
            _handler = handler;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var query = new GetAboutByIdQuery(1); 
            var value = await _handler.Handle(query);
            return View(value);
        }
    }
}