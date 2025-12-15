namespace CQRSRentACar.CQRSPattern.Results.BookingResults
{
    public class GetAvailableCarsQueryResult
    {
        public int CarId { get; set; }
        public string CarImage { get; set; }
        public string CarBrand { get; set; }
        public string CarModel { get; set; }
        public string CarSeat { get; set; }
        public string CarTransmission { get; set; }
        public string CarFuel { get; set; }
        public decimal CarPrice { get; set; }
        public decimal CarReview { get; set; }
    }
}