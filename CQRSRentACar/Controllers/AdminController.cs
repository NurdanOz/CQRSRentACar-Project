using CQRSRentACar.Context;
using CQRSRentACar.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CQRSRentACar.CQRSPattern.Handlers.FuelPriceHandlers;
using CQRSRentACar.CQRSPattern.Queries.FuelPriceQueries;
using System.Net.Mail;
using System.Net;
using CQRSRentACar.CQRSPattern.Commands.BookingCommand;
using CQRSRentACar.CQRSPattern.Handlers.BookingHandlers;
using CQRSRentACar.CQRSPattern.Queries.BookingQueries;
using CQRSRentACar.CQRSPattern.Handlers.CarHandlers;
using CQRSRentACar.CQRSPattern.Queries.CarQueries;
using CQRSRentACar.CQRSPattern.Commands.CarCommand;
using CQRSRentACar.CQRSPattern.Commands.AboutCommand;
using CQRSRentACar.CQRSPattern.Handlers.AboutHandlers;
using CQRSRentACar.CQRSPattern.Queries.AboutQueries;
using CQRSRentACar.CQRSPattern.Commands.ServiceCommand;
using CQRSRentACar.CQRSPattern.Handlers.ServiceHandlers;
using CQRSRentACar.CQRSPattern.Queries.ServiceQueries;
using CQRSRentACar.CQRSPattern.Commands.FeatureCommand;
using CQRSRentACar.CQRSPattern.Handlers.FeatureHandlers;
using CQRSRentACar.CQRSPattern.Queries.FeatureQueries;
using CQRSRentACar.CQRSPattern.Commands.SliderCommand;
using CQRSRentACar.CQRSPattern.Handlers.SliderHandlers;
using CQRSRentACar.CQRSPattern.Queries.SliderQueries;
using CQRSRentACar.CQRSPattern.Commands.EmployeeCommand;
using CQRSRentACar.CQRSPattern.Handlers.EmployeeHandlers;
using CQRSRentACar.CQRSPattern.Queries.EmployeeQueries;
using CQRSRentACar.CQRSPattern.Commands.TestimonialCommand;
using CQRSRentACar.CQRSPattern.Handlers.TestimonialHandlers;
using CQRSRentACar.CQRSPattern.Queries.TestimonialQueries;

namespace CQRSRentACar.Controllers
{
    public class AdminController : Controller
    {
        private readonly CarContext _context;
        private readonly IConfiguration _configuration;
        private readonly GetFuelPricesQueryHandler _fuelPricesHandler;

        public AdminController(CarContext context, IConfiguration configuration, GetFuelPricesQueryHandler fuelPricesHandler)
        {
            _context = context;
            _configuration = configuration;
            _fuelPricesHandler = fuelPricesHandler;
        }

        public async Task<IActionResult> Dashboard()
        {
            try
            {
                var testFuelPrices = new List<FuelPrice>
                {
                    new FuelPrice { FuelType = "BENZİN (GASOLINE)", Price = 27.69m },
                    new FuelPrice { FuelType = "MOTORİN (DIESEL)", Price = 33.60m },
                    new FuelPrice { FuelType = "ORTA BENZİN (MIDGRADE)", Price = 32.31m },
                    new FuelPrice { FuelType = "PREMİUM BENZİN", Price = 35.56m }
                };

                return View(testFuelPrices);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Yakıt fiyatları hatası: {ex.Message}");
                return View(new List<FuelPrice>());
            }
        }

        public IActionResult CarRecommendation()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetRecommendation(string userPrompt)
        {
            if (string.IsNullOrWhiteSpace(userPrompt))
            {
                return Json(new { success = false, message = "Lütfen bir açıklama girin." });
            }

            var cars = await _context.Cars
                .Select(c => new CarInfo
                {
                    Id = c.CarId,
                    Brand = c.CarBrand,
                    Model = c.CarModel,
                    Seat = c.CarSeat,
                    Transmission = c.CarTransmission,
                    Fuel = c.CarFuel,
                    Price = c.CarPrice
                })
                .ToListAsync();

            if (!cars.Any())
            {
                return Json(new { success = false, message = "Sistemde araç bulunamadı." });
            }

            var apiKey = _configuration["GeminiSettings:ApiKey"];
            var geminiHelper = new GeminiHelper(apiKey);
            var recommendation = await geminiHelper.GetCarRecommendation(userPrompt, cars);

            return Json(new { success = true, recommendation = recommendation });
        }



        // Mesajlar Listesi
        public async Task<IActionResult> Messages()
        {
            try
            {
                var messages = await _context.Messages
                    .AsNoTracking() // BU SATIRI EKLE
                    .Select(m => new Entities.Message // PROJECTION İLE ÇEK
                    {
                        MessageId = m.MessageId,
                        FullName = m.FullName ?? "",
                        Email = m.Email ?? "",
                        Subject = m.Subject ?? "",
                        MessageDetail = m.MessageDetail ?? "",
                        Phone = m.Phone ?? "",
                        AIResponse = m.AIResponse ?? "",
                        EmailSent = m.EmailSent,
                        EmailSentDate = m.EmailSentDate
                    })
                    .OrderByDescending(m => m.MessageId)
                    .ToListAsync();

                Console.WriteLine($"Mesaj sayısı: {messages.Count}");
                return View(messages);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"HATA: {ex.Message}");
                return View(new List<Entities.Message>());
            }
        }


        [HttpGet]
        public async Task<IActionResult> MessageDetail(int id)
        {
            var message = await _context.Messages.FindAsync(id);
            if (message == null)
            {
                return NotFound();
            }

            try
            {
              
                if (!message.EmailSent)
                {
                    var apiKey = _configuration["GeminiSettings:ApiKey"];
                    var messageHelper = new MessageReplyHelper(apiKey);

                    var autoReply = await messageHelper.GenerateAutoReply(
                        message.MessageDetail,
                        message.FullName,
                        message.Email,
                        message.Subject
                    );

                    // AI cevabını kaydet
                    message.AIResponse = autoReply;

                    // Email gönder
                    var smtpClient = new SmtpClient(_configuration["EmailSettings:SmtpServer"])
                    {
                        Port = int.Parse(_configuration["EmailSettings:Port"]),
                        Credentials = new NetworkCredential(
                            _configuration["EmailSettings:Username"],
                            _configuration["EmailSettings:Password"]
                        ),
                        EnableSsl = true,
                    };

                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress(_configuration["EmailSettings:FromEmail"], "Cental Rent A Car"),
                        Subject = $"Re: {message.Subject}",
                        Body = autoReply,
                        IsBodyHtml = false,
                    };
                    mailMessage.To.Add(message.Email);

                    await smtpClient.SendMailAsync(mailMessage);

                    // Email durumunu güncelle
                    message.EmailSent = true;
                    message.EmailSentDate = DateTime.Now;
                    await _context.SaveChangesAsync();

                    ViewBag.AutoReply = autoReply;
                    ViewBag.EmailStatus = "✅ Email otomatik olarak gönderildi!";
                }
                else
                {
                    // Daha önce gönderilmişse kayıtlı cevabı göster
                    ViewBag.AutoReply = message.AIResponse;
                    ViewBag.EmailStatus = $"✅ Email zaten gönderildi ({message.EmailSentDate?.ToString("dd.MM.yyyy HH:mm")})";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"AI/Email Error: {ex.Message}");
                ViewBag.AutoReply = "Otomatik cevap oluşturulamadı.";
                ViewBag.EmailStatus = $"❌ Hata: {ex.Message}";
            }

            return View(message);
        }

        [HttpPost]
        public async Task<IActionResult> SendEmail([FromBody] EmailRequest request)
        {
            try
            {
                var message = await _context.Messages.FindAsync(request.MessageId);
                if (message == null)
                {
                    return Json(new { success = false, message = "Mesaj bulunamadı." });
                }

                var smtpClient = new SmtpClient(_configuration["EmailSettings:SmtpServer"])
                {
                    Port = int.Parse(_configuration["EmailSettings:Port"]),
                    Credentials = new NetworkCredential(
                        _configuration["EmailSettings:Username"],
                        _configuration["EmailSettings:Password"]
                    ),
                    EnableSsl = true,
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_configuration["EmailSettings:FromEmail"], "Cental Rent A Car"),
                    Subject = $"Re: {message.Subject}",
                    Body = request.EmailContent,
                    IsBodyHtml = false,
                };
                mailMessage.To.Add(message.Email);

                await smtpClient.SendMailAsync(mailMessage);

                return Json(new { success = true, message = "Email başarıyla gönderildi!" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Email gönderme hatası: {ex.Message}");
                return Json(new { success = false, message = "Email gönderilemedi: " + ex.Message });
            }
        }


        // Rezervasyonlar Listesi
        public async Task<IActionResult> Bookings()
        {
            var handler = new GetAllBookingsQueryHandler(_context);
            var values = await handler.Handle(new GetAllBookingsQuery());
            return View(values);
        }

        [HttpPost]
        public async Task<IActionResult> ApproveBooking(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking != null)
            {
                booking.Status = "Onaylandı";
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Bookings");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            var handler = new RemoveBookingCommandHandler(_context);
            await handler.Handle(new RemoveBookingCommand(id));
            return RedirectToAction("Bookings");
        }

        // Araçlar Listesi
        public async Task<IActionResult> Cars()
        {
            var handler = new GetCarQueryHandler(_context);
            var values = await handler.Handle();
            return View(values);
        }

        // Araç Ekleme Sayfası
        [HttpGet]
        public IActionResult CreateCar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCar(CreateCarCommand command)
        {
            var handler = new CreateCarCommandHandler(_context);
            await handler.Handle(command);
            return RedirectToAction("Cars");
        }

        // Araç Güncelleme Sayfası
        [HttpGet]
        public async Task<IActionResult> UpdateCar(int id)
        {
            var handler = new GetCarByIdQueryHandler(_context);
            var value = await handler.Handle(new GetCarByIdQuery(id));
            return View(value);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCar(UpdateCarCommand command)
        {
            var handler = new UpdateCarCommandHandler(_context);
            await handler.Handle(command);
            return RedirectToAction("Cars");
        }

        // Araç Silme
        [HttpPost]
        public async Task<IActionResult> DeleteCar(int id)
        {
            var handler = new RemoveCarCommandHandler(_context);
            await handler.Handle(new RemoveCarCommand(id));
            return RedirectToAction("Cars");
        }

        // Hakkımızda Listesi
        public async Task<IActionResult> Abouts()
        {
            var handler = new GetAboutQueryHandler(_context);
            var values = await handler.Handle();
            return View(values);
        }

        // Hakkımızda Güncelleme
        [HttpGet]
        public async Task<IActionResult> UpdateAbout(int id)
        {
            var handler = new GetAboutByIdQueryHandler(_context);
            var value = await handler.Handle(new GetAboutByIdQuery(id));
            return View(value);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAbout(UpdateAboutCommand command)
        {
            var handler = new UpdateAboutCommandHandler(_context);
            await handler.Handle(command);
            return RedirectToAction("Abouts");
        }

        // Hizmetler Listesi
        public async Task<IActionResult> Services()
        {
            var handler = new GetServiceQueryHandler(_context);
            var values = await handler.Handle();
            return View(values);
        }

        // Hizmet Güncelleme
        [HttpGet]
        public async Task<IActionResult> UpdateService(int id)
        {
            var handler = new GetServiceByIdQueryHandler(_context);
            var value = await handler.Handle(new GetServiceByIdQuery(id));
            return View(value);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateService(UpdateServiceCommand command)
        {
            var handler = new UpdateServiceCommandHandler(_context);
            await handler.Handle(command);
            return RedirectToAction("Services");
        }


        // Özellikler Listesi
        public async Task<IActionResult> Features()
        {
            var handler = new GetFeatureQueryHandler(_context);
            var values = await handler.Handle();
            return View(values);
        }

        // Özellik Güncelleme
        [HttpGet]
        public async Task<IActionResult> UpdateFeature(int id)
        {
            var handler = new GetFeatureByIdQueryHandler(_context);
            var value = await handler.Handle(new GetFeatureByIdQuery(id));
            return View(value);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateFeature(UpdateFeatureCommand command)
        {
            var handler = new UpdateFeatureCommandHandler(_context);
            await handler.Handle(command);
            return RedirectToAction("Features");
        }


        // Sliderlar Listesi
        public async Task<IActionResult> Sliders()
        {
            var handler = new GetSliderQueryHandler(_context);
            var values = await handler.Handle();
            return View(values);
        }

        // Slider Güncelleme
        [HttpGet]
        public async Task<IActionResult> UpdateSlider(int id)
        {
            var handler = new GetSliderByIdQueryHandler(_context);
            var value = await handler.Handle(new GetSliderByIdQuery(id));
            return View(value);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateSlider(UpdateSliderCommand command)
        {
            var handler = new UpdateSliderCommandHandler(_context);
            await handler.Handle(command);
            return RedirectToAction("Sliders");
        }

        // Personeller Listesi
        public async Task<IActionResult> Employees()
        {
            var handler = new GetEmployeeQueryHandler(_context);
            var values = await handler.Handle();
            return View(values);
        }

        // Personel Ekleme
        [HttpGet]
        public IActionResult CreateEmployee()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee(CreateEmployeeCommand command)
        {
            var handler = new CreateEmployeeCommandHandler(_context);
            await handler.Handle(command);
            return RedirectToAction("Employees");
        }

        // Personel Güncelleme
        [HttpGet]
        public async Task<IActionResult> UpdateEmployee(int id)
        {
            var handler = new GetEmployeeByIdQueryHandler(_context);
            var value = await handler.Handle(new GetEmployeeByIdQuery(id));
            return View(value);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateEmployee(UpdateEmployeeCommand command)
        {
            var handler = new UpdateEmployeeCommandHandler(_context);
            await handler.Handle(command);
            return RedirectToAction("Employees");
        }

        // Personel Silme
        [HttpPost]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var handler = new RemoveEmployeeCommandHandler(_context);
            await handler.Handle(new RemoveEmployeeCommand(id));
            return RedirectToAction("Employees");
        }


        // Yorumlar Listesi
        public async Task<IActionResult> Testimonials()
        {
            var handler = new GetTestimonialQueryHandler(_context);
            var values = await handler.Handle();
            return View(values);
        }

        // Yorum Ekleme
        [HttpGet]
        public IActionResult CreateTestimonial()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateTestimonial(CreateTestimonialCommand command)
        {
            var handler = new CreateTestimonialCommandHandler(_context);
            await handler.Handle(command);
            return RedirectToAction("Testimonials");
        }

        // Yorum Güncelleme
        [HttpGet]
        public async Task<IActionResult> UpdateTestimonial(int id)
        {
            var handler = new GetTestimonialByIdQueryHandler(_context);
            var value = await handler.Handle(new GetTestimonialByIdQuery(id));
            return View(value);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTestimonial(UpdateTestimonialCommand command)
        {
            var handler = new UpdateTestimonialCommandHandler(_context);
            await handler.Handle(command);
            return RedirectToAction("Testimonials");
        }

        // Yorum Silme
        [HttpPost]
        public async Task<IActionResult> DeleteTestimonial(int id)
        {
            var handler = new RemoveTestimonialCommandHandler(_context);
            await handler.Handle(new RemoveTestimonialCommand(id));
            return RedirectToAction("Testimonials");
        }

        public class EmailRequest
        {
            public int MessageId { get; set; }
            public string EmailContent { get; set; }
        }
    }
}