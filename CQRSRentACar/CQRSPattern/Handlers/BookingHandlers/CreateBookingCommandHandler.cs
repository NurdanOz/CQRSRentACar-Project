using CQRSRentACar.Context;
using CQRSRentACar.CQRSPattern.Commands.BookingCommand;
using CQRSRentACar.Entities;
using System.Text.Json;

namespace CQRSRentACar.CQRSPattern.Handlers.BookingHandlers
{
    public class CreateBookingCommandHandler
    {
        private readonly CarContext _context;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public CreateBookingCommandHandler(
            CarContext context,
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration)
        {
            _context = context;
            _httpClient = httpClientFactory.CreateClient();
            _configuration = configuration;
        }

        public async Task Handle(CreateBookingCommand command)
        {
            decimal totalPrice = command.TotalPrice;
            decimal extraLocationFee = 0;

            // ✅ Farklı lokasyon kontrolü
            if (command.PickUpLocation != command.DropOffLocation)
            {
                Console.WriteLine("🚗 Farklı lokasyon tespit edildi! Mesafe hesaplanıyor...");

                try
                {
                    // Distance API'yi çağır
                    var distance = await CalculateDistance(
                        command.PickUpLocation,
                        command.DropOffLocation
                    );

                    Console.WriteLine($"✅ Mesafe hesaplandı: {distance} km");

                    // Yakıt ve ekstra ücret hesapla (varsayılan GASOLINE)
                    string fuelType = "GASOLINE";
                    var fuelPrice = GetFuelPrice(fuelType);
                    double avgConsumption = 7.0;

                    decimal fuelCost = (decimal)((distance / 100) * avgConsumption) * fuelPrice;
                    extraLocationFee = (decimal)distance * 0.5m; // km başına 0.5 TL

                    decimal extraTotal = fuelCost + extraLocationFee;
                    totalPrice += extraTotal;

                    Console.WriteLine($"💰 Yakıt ücreti: {fuelCost:F2} TL");
                    Console.WriteLine($"💰 Ekstra mesafe ücreti: {extraLocationFee:F2} TL");
                    Console.WriteLine($"💰 Toplam ekstra: {extraTotal:F2} TL");
                    Console.WriteLine($"💰 Yeni toplam fiyat: {totalPrice:F2} TL");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"❌ Mesafe hesaplama hatası: {ex.Message}");
                    // Hata durumunda varsayılan ekstra ücret
                    extraLocationFee = 500m;
                    totalPrice += extraLocationFee;
                    Console.WriteLine($"⚠️ Varsayılan ekstra ücret eklendi: {extraLocationFee} TL");
                }
            }

            // Booking kaydet
            _context.Bookings.Add(new Booking
            {
                Name = command.Name,
                Email = command.Email,
                Phone = command.Phone,
                PickUpDate = command.PickUpDate,
                DropOffDate = command.DropOffDate,
                PickUpLocation = command.PickUpLocation,
                DropOffLocation = command.DropOffLocation,
                CarId = command.CarId,
                CarBrand = command.CarBrand,
                CarModel = command.CarModel,
                TotalPrice = totalPrice, // ✅ Güncellenmiş fiyat
                Status = "Pending",
                CreatedDate = DateTime.Now
            });

            await _context.SaveChangesAsync();

            Console.WriteLine("✅ Rezervasyon başarıyla oluşturuldu!");
        }

        private async Task<double> CalculateDistance(string fromCity, string toCity)
        {
            var fromCoords = GetCityCoordinates(fromCity);
            var toCoords = GetCityCoordinates(toCity);

            Console.WriteLine($"📍 {fromCity} → ({fromCoords.lat}, {fromCoords.lon})");
            Console.WriteLine($"📍 {toCity} → ({toCoords.lat}, {toCoords.lon})");

            // Kuş uçuşu mesafe hesaplama (Haversine formülü)
            double R = 6371; // Dünya'nın yarıçapı (km)

            double lat1 = fromCoords.lat * Math.PI / 180;
            double lat2 = toCoords.lat * Math.PI / 180;
            double dLat = (toCoords.lat - fromCoords.lat) * Math.PI / 180;
            double dLon = (toCoords.lon - fromCoords.lon) * Math.PI / 180;

            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                      Math.Cos(lat1) * Math.Cos(lat2) *
                      Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            double distance = R * c;

            // Yol mesafesi genellikle kuş uçuşundan %20-30 daha fazla
            double roadDistance = distance * 1.25;

            Console.WriteLine($"✅ Kuş uçuşu mesafe: {distance:F1} km");
            Console.WriteLine($"✅ Tahmini yol mesafesi: {roadDistance:F1} km");

            return await Task.FromResult(roadDistance);
        }

        private (double lat, double lon) GetCityCoordinates(string cityName)
        {
            cityName = cityName.Replace("Havalimanı", "")
                               .Replace("Havalimani", "")
                               .Replace("(IST)", "").Replace("(ADA)", "")
                               .Replace("(ESB)", "").Replace("(AYT)", "")
                               .Replace("(SAW)", "").Replace("(ADB)", "")
                               .Trim();

            cityName = cityName.Replace("ı", "i").Replace("İ", "I")
                               .Replace("ş", "s").Replace("Ş", "S")
                               .Replace("ğ", "g").Replace("Ğ", "G")
                               .Replace("ü", "u").Replace("Ü", "U")
                               .Replace("ö", "o").Replace("Ö", "O")
                               .Replace("ç", "c").Replace("Ç", "C");

            var coordinates = new Dictionary<string, (double lat, double lon)>()
            {
                { "Istanbul", (41.0082, 28.9784) },
                { "Ankara", (39.9334, 32.8597) },
                { "Izmir", (38.4237, 27.1428) },
                { "Antalya", (36.8969, 30.7133) },
                { "Adana", (37.0000, 35.3213) },
                { "Bursa", (40.1826, 29.0665) },
                { "Gaziantep", (37.0662, 37.3833) },
                { "Konya", (37.8667, 32.4833) },
                { "Kayseri", (38.7205, 35.4826) },
                { "Mersin", (36.8000, 34.6333) },
                { "Eskisehir", (39.7767, 30.5206) },
                { "Diyarbakir", (37.9144, 40.2306) },
                { "Samsun", (41.2867, 36.33) },
                { "Denizli", (37.7765, 29.0864) },
                { "Trabzon", (41.0015, 39.7178) }
            };

            if (coordinates.ContainsKey(cityName))
            {
                return coordinates[cityName];
            }

            Console.WriteLine($"⚠️ Şehir bulunamadı: {cityName}, varsayılan Istanbul");
            return (41.0082, 28.9784);
        }

        private decimal GetFuelPrice(string fuelType)
        {
            return fuelType == "GASOLINE" ? 55.00m : 57.00m;
        }
    }
}