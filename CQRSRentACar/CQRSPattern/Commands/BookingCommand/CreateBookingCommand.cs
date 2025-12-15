namespace CQRSRentACar.CQRSPattern.Commands.BookingCommand
{
    public class CreateBookingCommand
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime PickUpDate { get; set; }
        public DateTime DropOffDate { get; set; }
        public string PickUpLocation { get; set; }
        public string DropOffLocation { get; set; }
        public int CarId { get; set; }
        public string CarBrand { get; set; }
        public string CarModel { get; set; }
        public decimal TotalPrice { get; set; }
    }
}