using Microsoft.AspNetCore.Mvc;
using CQRSRentACar.CQRSPattern.Commands.MessageCommand;
using CQRSRentACar.CQRSPattern.Handlers.MessageHandlers;
using CQRSRentACar.CQRSPattern.Handlers.AirportHandlers;
using CQRSRentACar.CQRSPattern.Queries.AirportQueries;

namespace CQRSRentACar.Controllers
{
    public class DefaultController : Controller
    {
        private readonly CreateMessageCommandHandler _createMessageCommandHandler;
        private readonly GetAirportsByCountryQueryHandler _airportHandler;

        public DefaultController(
            CreateMessageCommandHandler createMessageCommandHandler,
            GetAirportsByCountryQueryHandler airportHandler)
        {
            _createMessageCommandHandler = createMessageCommandHandler;
            _airportHandler = airportHandler;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Contact(CreateMessageCommand command)
        {
            await _createMessageCommandHandler.Handle(command);
            return RedirectToAction("Contact");
        }

        // Havalimanlarını getir (AJAX için)
        [HttpGet]
        public async Task<IActionResult> GetTurkeyAirports()
        {
            try
            {
                var query = new GetAirportsByCountryQuery("TR");
                var result = await _airportHandler.Handle(query);

                if (result.Success)
                {
                   
                    var airports = result.Airports.Select(a => new
                    {
                        code = a.Iata,
                        name = $"{a.Name} ({a.Iata})"
                    }).ToList();

                    return Json(new { success = true, data = airports });
                }
                else
                {
                    return Json(new { success = false, message = result.Message });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}