using CQRSRentACar.CQRSPattern.Commands.BookingCommand;
using CQRSRentACar.CQRSPattern.Handlers.BookingHandlers;
using CQRSRentACar.CQRSPattern.Queries.BookingQueries;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CQRSRentACar.Controllers
{
    public class BookingController : Controller
    {
        private readonly GetAvailableCarsQueryHandler _getAvailableCarsQueryHandler;
        private readonly CreateBookingCommandHandler _createBookingCommandHandler;

        public BookingController(
            GetAvailableCarsQueryHandler getAvailableCarsQueryHandler,
            CreateBookingCommandHandler createBookingCommandHandler)
        {
            _getAvailableCarsQueryHandler = getAvailableCarsQueryHandler;
            _createBookingCommandHandler = createBookingCommandHandler;
        }

        // HTML Entity'leri decode eden helper metod
        private string DecodeHtmlEntities(string text)
        {
            if (string.IsNullOrEmpty(text))
                return text;

            return WebUtility.HtmlDecode(text);
        }

        [HttpPost]
        public async Task<IActionResult> AvailableCars(DateTime pickUpDate, DateTime dropOffDate, string pickUpTime, string dropOffTime, string pickUpLocation, string dropOffLocation)
        {
            var pickUpDateTime = pickUpDate.Add(TimeSpan.Parse(pickUpTime));
            var dropOffDateTime = dropOffDate.Add(TimeSpan.Parse(dropOffTime));

            if (string.IsNullOrEmpty(dropOffLocation))
            {
                dropOffLocation = pickUpLocation;
            }

            
            pickUpLocation = DecodeHtmlEntities(pickUpLocation);
            dropOffLocation = DecodeHtmlEntities(dropOffLocation);

            var query = new GetAvailableCarsQuery(pickUpDateTime, dropOffDateTime, pickUpLocation);
            var values = await _getAvailableCarsQueryHandler.Handle(query);

            ViewBag.PickUpDate = pickUpDateTime;
            ViewBag.DropOffDate = dropOffDateTime;
            ViewBag.PickUpLocation = pickUpLocation;
            ViewBag.DropOffLocation = dropOffLocation;

            return View(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBooking(CreateBookingCommand command)
        {
            await _createBookingCommandHandler.Handle(command);
            return RedirectToAction("BookingSuccess");
        }

        public IActionResult BookingSuccess()
        {
            return View();
        }
    }
}