using CQRSRentACar.CQRSPattern.Handlers.StatisticHandlers;
using CQRSRentACar.CQRSPattern.Queries.StatisticQueries;
using Microsoft.AspNetCore.Mvc;

namespace CQRSRentACar.ViewComponents
{
    public class UStatisticsViewComponent : ViewComponent
    {
        private readonly GetStatisticQueryHandler _handler;

        public UStatisticsViewComponent(GetStatisticQueryHandler handler)
        {
            _handler = handler;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var query = new GetStatisticQuery();
            var values = _handler.Handle(query);
            return View(values);
        }
    }
}